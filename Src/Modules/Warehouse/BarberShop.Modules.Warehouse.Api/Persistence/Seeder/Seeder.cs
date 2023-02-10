using BarberShop.Shared;
using Bogus;

namespace BarberShop.Modules.Warehouse.Api.Persistence.Seeder;

internal static class Seeder
{
    public static void Seed(WarehouseDbContext dbContext)
    {
        Randomizer.Seed = new Random(StaticVariables.Seed);
        ProductSeed.SeedProducts(dbContext);
    }
}