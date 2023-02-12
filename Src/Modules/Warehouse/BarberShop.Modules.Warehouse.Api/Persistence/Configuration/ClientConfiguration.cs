using BarberShop.Modules.Warehouse.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.Warehouse.Api.Persistence.Configuration;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(p => p.FullName).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.NumberPhone).IsRequired();

        builder
            .HasMany(p => p.Orders)
            .WithOne(p => p.Client)
            .HasPrincipalKey(p=>p.Id)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}