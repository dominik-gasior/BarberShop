using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class ServiceIndustryConfiguration : IEntityTypeConfiguration<ServiceIndustry>
{
    public void Configure(EntityTypeBuilder<ServiceIndustry> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Time).IsRequired();
    }
}