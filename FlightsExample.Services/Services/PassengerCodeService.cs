using FlightsExample.Core.Dtos;
using FlightsExample.Core.Services;
using System.Text;

namespace FlightsExample.Services.Services
{
    public class PassengerCodeService : IPassengerCodeService
    {
        //TODO: If I had more time I would move it to separate static file and read configuration from it
        private readonly IDictionary<Destination, string> destinationDictionary = new Dictionary<Destination, string>()
        {
            { Destination.UK, "U" },
            { Destination.Europe, "E" },
            { Destination.Asia, "A" },
            { Destination.America, "Z" }
        };
        private readonly IDictionary<Meal, string> mealDictionary = new Dictionary<Meal, string>()
        {
            { Meal.European, "G" },
            { Meal.EuropeanChild, "g" },
            { Meal.Vegeterian, "K" },
            { Meal.VegeterianChild, "k" },
            { Meal.Asian, "H" },
            { Meal.AsianChild, "h" },
        };
        private readonly IDictionary<Gender, string> genderDictionary = new Dictionary<Gender, string>()
        {
            { Gender.Male, "X" },
            { Gender.Female, "Y" }
        };
        private readonly IDictionary<FlightClass, string> flightClassDictionary = new Dictionary<FlightClass, string>()
        {
            { FlightClass.First, "P" },
            { FlightClass.Business, "Q" },
            { FlightClass.Economy, "R" }
        };

        public CreatePassengerCodeResponse Create(CreatePassengerCodeRequest createPassengerCodeRequest)
        {
            var validationResult = Validate(createPassengerCodeRequest);
            if(!validationResult.Item1)
            {
                return new CreatePassengerCodeResponse()
                {
                    Success = false,
                    ErrorMessage = validationResult.Item2,
                };
            }
            var sb = new StringBuilder();
            var destinationCode = destinationDictionary[createPassengerCodeRequest.Destination];
            sb.Append(CheckIfNightFlight(createPassengerCodeRequest.StartTime, createPassengerCodeRequest.EndTime) ? destinationCode.ToLower() : destinationCode);
            var genderCode = genderDictionary[createPassengerCodeRequest.Gender];
            var isChild = CheckIfChild(createPassengerCodeRequest.Age);
            sb.Append(isChild ? genderCode.ToLower() : genderCode);
            var mealCode = mealDictionary[createPassengerCodeRequest.Meal];
            sb.Append(mealCode);
            sb.Append(flightClassDictionary[createPassengerCodeRequest.FlightClass]);
            sb.Append(IsFromEu(createPassengerCodeRequest.Source) ? "-EU" : "-ZZ");
            return new CreatePassengerCodeResponse()
            {
                Success = true,
                PassengerCode = sb.ToString()
            };
        }

        private bool CheckIfNightFlight(DateTime startTime, DateTime endTime)
        {
            TimeSpan startTimeSpan = new TimeSpan(22, 0, 0);
            TimeSpan endTimeSpan = new TimeSpan(6, 0, 0);
            TimeSpan start = startTime.TimeOfDay;
            TimeSpan end = endTime.TimeOfDay;

            if ((start >= startTimeSpan || start < endTimeSpan) && (end >= startTimeSpan || end < endTimeSpan))
            {
                return true;
            }

            return false;
        }

        private bool CheckIfChild(int age)
        {
            return age < 12;
        }

        private bool IsFromEu(Destination destination) 
        {
            return destination == Destination.Europe;
        }

        private Tuple<bool, string> Validate(CreatePassengerCodeRequest createPassengerCodeRequest)
        {
            if (createPassengerCodeRequest.Age < 1)
            {
                return new Tuple<bool, string>(false, "Person is below 12 month old");
            }
            if (createPassengerCodeRequest.Age > 80)
            {
                return new Tuple<bool, string>(false, "Person is above 80 years old");
            }
            var childMeals = new List<Meal>() { Meal.AsianChild, Meal.EuropeanChild, Meal.VegeterianChild };
            if (createPassengerCodeRequest.Age > 18 && childMeals.Contains(createPassengerCodeRequest.Meal))
            {
                return new Tuple<bool, string>(false, "Adult cannot order child meal");
            }
            if (createPassengerCodeRequest.FlightClass == FlightClass.First &&
                (createPassengerCodeRequest.Source == Destination.UK || createPassengerCodeRequest.Destination == Destination.UK))
            {
                return new Tuple<bool, string>(false, "There is no business class for UK");
            }
            return new Tuple<bool, string>(true, string.Empty);
        }
    }
}