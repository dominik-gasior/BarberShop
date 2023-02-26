using BarberShop.Modules.Warehouse.Api.Entities;

namespace BarberShop.Modules.Warehouse.Api.Persistence.Seeder;

internal static class ProductSeed
{
    internal static void SeedProducts(WarehouseDbContext dbContext)
    {
        if (dbContext.Products.Any()) return;
        var products = new List<Product>()
        {
            new Product
            {
                Name = "Żel do włosów",
                Description = "Idealny do włosów zniszczonych",
                Price = 20.00m,
                LastPrice = 20.00m,
                Amount = 10,
                IsAvailable = true
            },
            new Product
            {
                Name = "Szampon do włosów",
                Description = "Idealny do włosów kręconych",
                Price = 25.49m,
                LastPrice = 25.49m,
                Amount = 50,
                IsAvailable = true
            },
            new Product
            {
                Name = "Odzywka do włosów",
                Description = "Idealny do włosów zniszczonych",
                Price = 9.99m,
                LastPrice = 9.99m,
                Amount = 100,
                IsAvailable = true
            },
            new Product
            {
                Name = "Grzebien do włosów",
                Description = "Wykonany z najwyzszej jakosci materialow",
                Price = 49.99m,
                LastPrice = 49.99m,
                Amount = 25,
                IsAvailable = true
            }
        };
        dbContext.AddRangeAsync(products);
        dbContext.SaveChangesAsync();
    }
}