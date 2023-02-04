using BarberShop.Modules.Warehouse.Api.Entities;
using Infrastructure.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.ClientId);
        
        builder.Property(p => p.Cost).IsRequired();
        builder.Property(p => p.DeliveryTime).IsRequired();
        builder.Property(p => p.ClientId).IsRequired();
    }
}