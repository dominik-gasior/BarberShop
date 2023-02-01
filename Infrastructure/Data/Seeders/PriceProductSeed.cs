using Src.Domain;

namespace Infrastructure.Data.Seeders;

internal static class PriceProductSeed
{
    internal static void SeedPriceProducts(AppDbContext dbContext)
    {
        if (dbContext.PriceProducts.Any()) return;
        var pricesProducts = new List<PriceProduct>
        {
            new PriceProduct
            {
                Price = 24.99m,
                LastPrice = 24.99m
            },
            new PriceProduct
            {
                Price = 44.99m,
                LastPrice = 34.99m
            },
            new PriceProduct
            {
                Price = 14.99m,
                LastPrice = 14.99m
            },
            new PriceProduct
            {
                Price = 64.99m,
                LastPrice = 74.99m
            },
        };
        dbContext.AddRangeAsync(pricesProducts);
        dbContext.SaveChangesAsync();
    }
}