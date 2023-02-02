using Infrastructure.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

internal class AmountProductConfiguration : IEntityTypeConfiguration<AmountProduct>
{
    public void Configure(EntityTypeBuilder<AmountProduct> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Amount).IsRequired();

        builder.HasOne(p => p.Product)
            .WithOne(p => p.AmountProduct)
            .HasForeignKey<Product>(p => p.AmountProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}