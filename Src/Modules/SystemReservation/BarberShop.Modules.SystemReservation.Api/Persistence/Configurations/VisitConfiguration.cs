using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Configurations;

internal class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.UserId);
        builder.HasIndex(p => p.EmployeeId);
        builder.HasIndex(p => p.ServiceIndustryId);
        
        builder.Property(p => p.EmployeeId).IsRequired();
        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.ServiceIndustryId).IsRequired();
        builder.Property(p => p.NumberPhone).IsRequired();
        
    }
}