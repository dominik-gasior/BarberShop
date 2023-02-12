using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Configurations;

internal sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Fullname).IsRequired();

        builder
            .HasMany(p => p.Visits)
            .WithOne(p => p.Employee)
            .HasPrincipalKey(p=>p.Id)
            .HasForeignKey(p => p.EmployeeGuid)
            .OnDelete(DeleteBehavior.Cascade);
    }
}