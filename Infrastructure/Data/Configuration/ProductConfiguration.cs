using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.PriceProductId);
        builder.HasIndex(p => p.AmountProductId);
        
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.PriceProductId).IsRequired();
        builder.Property(p => p.AmountProductId).IsRequired();

        builder.HasMany(p => p.Orders)
            .WithMany(p => p.Products);
    }
}