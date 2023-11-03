using FlightsExample.Core.Entities;
using FlightsExample.Infrastructure.Context.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FlightsExample.Infrastructure.Context
{
    public class PassengersContext : DbContext
    {
        // TODO: improve db schema - but is it really needed for small POC? If I would have to display users per each flight I would create separate
        // separate flights db table and add FK to its ID per each new passenger/reservation entry
        public PassengersContext()
        {
        }

        public PassengersContext(DbContextOptions<PassengersContext> options)
            : base(options)
        {
        }
        public DbSet<PassengerEntry> PassengerEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PassengerEntryConfiguration());
        }
    }
}