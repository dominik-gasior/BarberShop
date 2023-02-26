using BarberShop.Modules.Warehouse.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.Warehouse.Api.Persistence.Configuration;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.LastPrice).IsRequired();
        builder.Property(p => p.IsAvailable).IsRequired().HasDefaultValue(true);

        builder.HasMany(p => p.Orders)
            .WithMany(p => p.Products);
    }
}