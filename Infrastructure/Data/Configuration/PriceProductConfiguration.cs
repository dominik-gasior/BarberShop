using Infrastructure.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

internal class PriceProductConfiguration : IEntityTypeConfiguration<PriceProduct>
{
    public void Configure(EntityTypeBuilder<PriceProduct> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.LastPrice).IsRequired();

        builder.HasOne(p => p.Product)
            .WithOne(p => p.PriceProduct)
            .HasForeignKey<Product>(p => p.PriceProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}