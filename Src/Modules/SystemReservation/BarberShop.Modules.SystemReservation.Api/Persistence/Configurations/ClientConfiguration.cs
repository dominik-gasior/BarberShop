using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Fullname).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.NumberPhone).IsRequired();

        builder
            .HasMany(p =>p.Visits)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}