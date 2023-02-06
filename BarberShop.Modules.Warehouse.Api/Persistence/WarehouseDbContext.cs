using BarberShop.Modules.Warehouse.Api.Entities;
using Infrastructure.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Warehouse.Api.Persistence;

public class WarehouseDbContext : DbContext 
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PriceProduct> PriceProducts { get; set; }
    public DbSet<AmountProduct> AmountProducts { get; set; }
    
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BarberShop.Warehouse");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);
    }
        
}