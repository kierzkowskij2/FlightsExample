using FlightsExample.Core.Database.Repositories;
using FlightsExample.Core.Dtos;
using FlightsExample.Core.Entities;
using FlightsExample.Core.Services;

namespace FlightsExample.Services.Services
{
    //TODO: If I had more time for sure I would introduce mapper to map all dto<->dto and dto<->entity objects
    public class RegistrationService : IRegistrationService
    {
        private readonly IPassengerCodeService _passengerCodeService;
        private readonly IQRCodeService _qrCodeService;
        private readonly IPassengersRepostitory _passengerRepository;
        private readonly IMailService _mailService;
        public RegistrationService(IPassengerCodeService passengerCodeService,
            IQRCodeService qRCodeService,
            IPassengersRepostitory passengersRepostitory,
            IMailService mailService) 
        {
            _passengerCodeService = passengerCodeService;
            _qrCodeService = qRCodeService;
            _passengerRepository = passengersRepostitory;
            _mailService = mailService;
        }
        public async Task<RegisterResultDto> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                var createPassengerCodeRequest = new CreatePassengerCodeRequest()
                {
                    Age = CalculateAgeInYears(registerRequestDto.BirthDate),
                    Destination = registerRequestDto.Destination,
                    Source = registerRequestDto.Source,
                    EndTime = registerRequestDto.EndTime,
                    StartTime = registerRequestDto.StartTime,
                    FlightClass = registerRequestDto.FlightClass,
                    Gender = registerRequestDto.Gender,
                    Meal = registerRequestDto.Meal
                };
                var createPassengerCodeResponse = _passengerCodeService.Create(createPassengerCodeRequest);

                if(!createPassengerCodeResponse.Success)
                {
                    return new RegisterResultDto()
                    {
                        ErrorMessage = createPassengerCodeResponse.ErrorMessage,
                        Success = false
                    };
                }

                var passengerEntry = new PassengerEntry()
                {
                    BirthDate = registerRequestDto.BirthDate,
                    Address = registerRequestDto.Address,
                    DestinationCode = (int)registerRequestDto.Destination,
                    SourceCode = (int)registerRequestDto.Source,
                    Email = registerRequestDto.Email,
                    Gender = registerRequestDto.Gender.ToString(),
                    Name = registerRequestDto.Name,
                    PassengerCode = createPassengerCodeResponse.PassengerCode
                };
                var result = await _passengerRepository.AddPassenger(passengerEntry);

                if(result == 0)
                {
                    return new RegisterResultDto()
                    {
                        ErrorMessage = "Data was not saved in database",
                        Success = false
                    };
                }

                var qrCode = _qrCodeService.Create(createPassengerCodeResponse.PassengerCode);

                var sendMailRequest = new SendMailRequest()
                {
                    Email = registerRequestDto.Email,
                    Name = registerRequestDto.Name,
                    QRCode = qrCode
                };
                var sendMailResult = await _mailService.Send(sendMailRequest);

                return new RegisterResultDto()
                {
                    QrCode = qrCode,
                    Success = sendMailResult
                };
            }
            catch (Exception ex)
            {
                return new RegisterResultDto()
                {
                    ErrorMessage = ex.Message,
                    Success = false
                };
            }
        }

        private int CalculateAgeInYears(DateTime birthdate)
        {
            var currentDate = DateTime.Now;
            int years = currentDate.Year - birthdate.Year;

            if (currentDate.Month < birthdate.Month || (currentDate.Month == birthdate.Month && currentDate.Day < birthdate.Day))
            {
                years--;
            }

            return years;
        }
    }
}