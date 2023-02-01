using Src.Domain;

namespace Infrastructure.Data.Seeders;

internal static class ProductSeed
{
    internal static void SeedProducts(AppDbContext dbContext)
    {
        if (dbContext.Products.Any()) return;
        var products = new List<Product>()
        {
            new Product
            {
                Name = "Żel do włosów",
                Description = "Idealny do włosów zniszczonych",
                PriceProductId = 1,
                AmountProductId = 1,
            },
            new Product
            {
                Name = "Szampon do włosów",
                Description = "Idealny do włosów kręconych",
                PriceProductId = 2,
                AmountProductId = 2,
            },
            new Product
            {
                Name = "Odzywka do włosów",
                Description = "Idealny do włosów zniszczonych",
                PriceProductId = 3,
                AmountProductId = 3,
            },
            new Product
            {
                Name = "Grzebien do włosów",
                Description = "Wykonany z najwyzszej jakosci materialow",
                PriceProductId = 4,
                AmountProductId = 4,
            }
        };
        dbContext.AddRangeAsync(products);
        dbContext.SaveChangesAsync();
    }
}