using Microsoft.EntityFrameworkCore;
using Src.Domain;
using Order = Src.Domain.Order;

namespace Src.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<VisitTime> VisitTimes { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<ServiceIndustry> ServiceIndustries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PriceProduct> PriceProducts { get; set; }
    public DbSet<AmountProduct> AmountProducts { get; set; }
    //TODO Change format datetime in Database. 
    //TODO End configuration (primary key etc.)

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}