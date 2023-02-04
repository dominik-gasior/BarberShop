using BarberShop.Shared;
using Bogus;
using Infrastructure.Data;

namespace BarberShop.Modules.Warehouse.Api.Persistence.Seeder;

public static class Seeder
{
    public static void Seed(WarehouseDbContext dbContext)
    {
        Randomizer.Seed = new Random(StaticVariables.Seed);
        PriceProductSeed.SeedPriceProducts(dbContext);
        AmountProductSeed.SeedAmountProducts(dbContext);
        ProductSeed.SeedProducts(dbContext);
    }
}