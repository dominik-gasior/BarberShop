using Infrastructure.Domain.SystemReservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

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