using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Configurations;

internal sealed class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.ClientGuid);
        builder.HasIndex(p => p.EmployeeGuid);
        builder.HasIndex(p => p.ServiceIndustryId);
        
        builder.Property(p => p.EmployeeGuid).IsRequired();
        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.ClientGuid).IsRequired();
        builder.Property(p => p.ServiceIndustryId).IsRequired();
    }
}