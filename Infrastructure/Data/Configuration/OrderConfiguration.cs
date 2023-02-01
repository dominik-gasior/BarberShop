using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Src.Domain.Order> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.ClientId);
        
        builder.Property(p => p.Cost).IsRequired();
        builder.Property(p => p.DeliveryTime).IsRequired();
        builder.Property(p => p.ClientId).IsRequired();
    }
}