using BarberShop.Shared;
using Bogus;
using Infrastructure.Data.Seeders;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Seeder;

internal static class Seeder
{
    internal static async Task Seed(this SystemReservationDbContext dbContext)
    {
        ServiceIndustrySeed.SeedServiceIndustries(dbContext);
        RoleSeed.SeedRoles(dbContext);
    }
    
}