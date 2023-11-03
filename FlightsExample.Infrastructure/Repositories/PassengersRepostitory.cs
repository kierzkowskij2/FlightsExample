using FlightsExample.Core.Database.Repositories;
using FlightsExample.Core.Entities;
using FlightsExample.Infrastructure.Context;

namespace FlightsExample.Infrastructure.Repositories
{
    public class PassengersRepostitory : IPassengersRepostitory
    {
        private readonly PassengersContext _passengersContext;
        public PassengersRepostitory(PassengersContext passengersContext)
        {
            _passengersContext = passengersContext;
        }
        public async Task<int> AddPassenger(PassengerEntry passengerEntry)
        {
            await _passengersContext.PassengerEntries.AddAsync(passengerEntry);
            return await _passengersContext.SaveChangesAsync();
        }
    }
}