using BarberShop.Modules.Warehouse.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Warehouse.Api.Persistence;

public class WarehouseDbContext : DbContext 
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BarberShop.Warehouse");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
    }
        
}