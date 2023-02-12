using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Persistence;

public sealed class SystemReservationDbContext : DbContext
{
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ServiceIndustry> ServiceIndustries { get; set; }
    public SystemReservationDbContext(DbContextOptions<SystemReservationDbContext> options) : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BarberShop.SystemReservation");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemReservationDbContext).Assembly);
    }
        
}