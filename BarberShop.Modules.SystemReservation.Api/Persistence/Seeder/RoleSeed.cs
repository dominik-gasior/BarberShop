using BarberShop.Modules.SystemReservation.Api.Persistence;
using Infrastructure.Domain.SystemReservation;

namespace Infrastructure.Data.Seeders;

internal static class RoleSeed
{
    internal static void SeedRoles(SystemReservationDbContext dbContext)
    {
        if (dbContext.Roles.Any()) return;
        var roles = new List<Role>()
        {
            new Role
            {
                Name = "Właściciel"
            },
            new Role
            {
                Name = "Sekretarka"
            },
            new Role
            {
                Name = "Pracownik"
            }
        };
        dbContext.AddRangeAsync(roles);
        dbContext.SaveChangesAsync();
    }
}