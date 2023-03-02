using BarberShop.Modules.Warehouse.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.Warehouse.Api.Persistence.Configuration;

internal sealed class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(p => new { p.OrderId, p.ProductId });

        builder.HasOne(p => p.Order)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(p => p.OrderId);

        builder.HasOne(p => p.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(p => p.ProductId);
    }
}