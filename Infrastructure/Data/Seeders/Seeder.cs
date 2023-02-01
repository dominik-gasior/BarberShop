using Bogus;
using Src.Domain;

namespace Infrastructure.Data.Seeders;

public static class Seeder
{
    public static void Seed(AppDbContext dbContext)
    {
        string locale = "pl";
        Randomizer.Seed = new Random(StaticVariables.Seed);

        ServiceIndustrySeed.SeedServiceIndustries(dbContext);
        RoleSeed.SeedRoles(dbContext);
        EmployeeSeed.SeedEmployees(dbContext,locale);
        ClientSeed.SeedClients(dbContext, locale);
        PriceProductSeed.SeedPriceProducts(dbContext);
        AmountProductSeed.SeedAmountProducts(dbContext);
        ProductSeed.SeedProducts(dbContext);
    }
}