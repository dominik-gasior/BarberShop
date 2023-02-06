using BarberShop.Modules.Users.Api.Entities;
using BarberShop.Shared;
using Bogus;

namespace BarberShop.Modules.Users.Api.Persistence.Seeder;

internal static class UserSeed
{
    internal static void SeedUsers(UsersDbContext dbContext, string locale)
    {
        if (dbContext.Users.Any()) return;
        var usersGenerator = new Faker<User>(locale)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.NumberPhone, f => f.Random.Number(StaticVariables.MinNumberPhone, StaticVariables.MaxNumberPhone).ToString());
        
        var users = usersGenerator.Generate(StaticVariables.ClientsGeneratorCount);
        dbContext.AddRangeAsync(users);
        dbContext.SaveChangesAsync();

    }
}