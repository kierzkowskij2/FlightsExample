using FlightsExample.Core.Dtos;

namespace FlightsExample.Core.Services
{
    public interface IPassengerCodeService
    {
        CreatePassengerCodeResponse Create(CreatePassengerCodeRequest createPassengerCodeRequest);
    }
}