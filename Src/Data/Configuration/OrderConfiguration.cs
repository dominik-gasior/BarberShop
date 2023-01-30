using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.Cost).IsRequired();
        builder.Property(p => p.DeliveryTime).IsRequired();
        builder.Property(p => p.ClientId).IsRequired();
    }
}