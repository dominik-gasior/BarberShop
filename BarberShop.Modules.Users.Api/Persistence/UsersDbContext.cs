using System.Reflection;
using BarberShop.Modules.Users.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Users.Api.Persistence;

internal class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (Database.IsRelational()) modelBuilder.HasDefaultSchema("BarberShop.Users");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
        
}