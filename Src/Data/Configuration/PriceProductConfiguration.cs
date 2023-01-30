using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Src.Domain;

namespace Src.Data.Configuration;

public class PriceProductConfiguration : IEntityTypeConfiguration<PriceProduct>
{
    public void Configure(EntityTypeBuilder<PriceProduct> builder)
    {
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.LastPrice).IsRequired();

        builder.HasOne(p => p.Product)
            .WithOne(p => p.PriceProduct)
            .HasForeignKey<Product>(p => p.PriceProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}