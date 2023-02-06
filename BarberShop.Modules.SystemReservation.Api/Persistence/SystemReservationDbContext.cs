using BarberShop.Modules.SystemReservation.Api.Entities;
using Infrastructure.Domain.SystemReservation;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Persistence;

public class SystemReservationDbContext : DbContext
{
    public DbSet<VisitTime> VisitTimes { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<ServiceIndustry> ServiceIndustries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public SystemReservationDbContext(DbContextOptions<SystemReservationDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BarberShop.SystemReservation");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemReservationDbContext).Assembly);
    }
        
}