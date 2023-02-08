using BarberShop.Shared;
using Bogus;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Seeder;

internal static class Seeder
{
    internal static void Seed(this SystemReservationDbContext dbContext)
    {
        ServiceIndustrySeed.SeedServiceIndustries(dbContext);
    }
    
}