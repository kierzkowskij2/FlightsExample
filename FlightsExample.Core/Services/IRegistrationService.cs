using FlightsExample.Core.Dtos;

namespace FlightsExample.Core.Services
{
    public interface IRegistrationService
    {
        Task<RegisterResultDto> Register(RegisterRequestDto registerRequestDto);
    }
}