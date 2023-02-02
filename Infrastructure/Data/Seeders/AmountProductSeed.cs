using Infrastructure.Domain.Warehouse;

namespace Infrastructure.Data.Seeders;

internal static class AmountProductSeed
{
    internal static void SeedAmountProducts(AppDbContext dbContext)
    {
        if (dbContext.AmountProducts.Any()) return;
        var amountsProducts = new List<AmountProduct>
        {
            new AmountProduct
            {
                Amount = 20,
            },
            new AmountProduct
            {
                Amount = 50,
            },
            new AmountProduct
            {
                Amount = 5,
            },
            new AmountProduct
            {
                Amount = 100,
            },
        };
        dbContext.AddRangeAsync(amountsProducts);
        dbContext.SaveChangesAsync();
    }
}