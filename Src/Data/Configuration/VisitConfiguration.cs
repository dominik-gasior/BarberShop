using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.Property(p => p.EmployeeId).IsRequired();
        builder.Property(p => p.VisitTimeId).IsRequired();
        builder.Property(p => p.ClientId).IsRequired();
        builder.Property(p => p.ServiceIndustryId).IsRequired();
    }
}