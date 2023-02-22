using BarberShop.Shared;
using Bogus;

namespace BarberShop.Modules.Warehouse.Api.Persistence.Seeder;

internal static class Seeder
{
    internal static void Seed(this WarehouseDbContext dbContext)
    {
        ProductSeed.SeedProducts(dbContext);
    }
    
}