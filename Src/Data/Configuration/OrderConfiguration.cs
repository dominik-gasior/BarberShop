using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Domain.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Order> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.ClientId);
        
        builder.Property(p => p.Cost).IsRequired();
        builder.Property(p => p.DeliveryTime).IsRequired();
        builder.Property(p => p.ClientId).IsRequired();
    }
}