using FlightsExample.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightsExample.Infrastructure.Context.Configuration
{
    public class PassengerEntryConfiguration : IEntityTypeConfiguration<PassengerEntry>
    {
        public void Configure(EntityTypeBuilder<PassengerEntry> entity)
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.BirthDate).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.PassengerCode).IsRequired();
            entity.Property(e => e.DestinationCode).IsRequired();
            entity.Property(e => e.SourceCode).IsRequired();
        }
    }
}