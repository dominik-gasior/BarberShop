using System.Reflection;
using BarberShop.Modules.Users.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Users.Api.Persistence;

internal sealed class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BarberShop.Users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
        
}