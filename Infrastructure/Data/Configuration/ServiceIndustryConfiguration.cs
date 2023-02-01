using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

internal class ServiceIndustryConfiguration : IEntityTypeConfiguration<ServiceIndustry>
{
    public void Configure(EntityTypeBuilder<ServiceIndustry> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Time).IsRequired();

        builder.HasMany(p => p.Visits)
            .WithOne(p => p.ServiceIndustry)
            .HasForeignKey(p => p.ServiceIndustryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}