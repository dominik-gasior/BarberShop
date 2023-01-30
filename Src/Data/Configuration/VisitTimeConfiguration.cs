using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class VisitTimeConfiguration : IEntityTypeConfiguration<VisitTime>
{
    public void Configure(EntityTypeBuilder<VisitTime> builder)
    {
        builder.Property(p => p.Date).IsRequired();

        builder.HasMany(p => p.Visits)
            .WithMany(p => p.VisitTimes);
    }
}