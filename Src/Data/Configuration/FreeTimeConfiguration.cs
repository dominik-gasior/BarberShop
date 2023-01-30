using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class FreeTimeConfiguration : IEntityTypeConfiguration<FreeTime>
{
    public void Configure(EntityTypeBuilder<FreeTime> builder)
    {
        builder.HasIndex(p => p.VisitId).IsUnique(false);
        builder.HasIndex(p => p.VisitTimeId).IsUnique(false);
    }
}