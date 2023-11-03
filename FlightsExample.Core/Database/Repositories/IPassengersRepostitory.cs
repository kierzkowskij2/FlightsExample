using FlightsExample.Core.Entities;

namespace FlightsExample.Core.Database.Repositories
{
    public interface IPassengersRepostitory
    {
        Task<int> AddPassenger(PassengerEntry passengerEntry);
    }
}