using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Configurations;

internal class VisitTimeConfiguration : IEntityTypeConfiguration<VisitTime>
{
    public void Configure(EntityTypeBuilder<VisitTime> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Date).IsRequired();

        builder.HasMany(p => p.Visits)
            .WithMany(p => p.VisitTimes);
    }
}