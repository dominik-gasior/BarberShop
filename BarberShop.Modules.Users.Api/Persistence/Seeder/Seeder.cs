using BarberShop.Shared;
using Bogus;

namespace BarberShop.Modules.Users.Api.Persistence.Seeder;

internal static class Seeder
{
    internal static async Task Seed(this UsersDbContext dbContext)
    {
        Randomizer.Seed = new Random(StaticVariables.Seed);
        UserSeed.SeedUsers(dbContext, StaticVariables.Locale);
    }
}