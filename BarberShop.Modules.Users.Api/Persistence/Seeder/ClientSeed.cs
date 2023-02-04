using BarberShop.Modules.Users.Api.Entities;
using BarberShop.Shared;
using Bogus;

namespace BarberShop.Modules.Users.Api.Persistence.Seeder;

internal static class ClientSeed
{
    internal static void SeedClients(UsersDbContext dbContext, string locale)
    {
        if (dbContext.Users.Any()) return;
        var clientsGenerator = new Faker<User>(locale)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.NumberPhone, f => f.Random.Number(StaticVariables.MinNumberPhone, StaticVariables.MaxNumberPhone).ToString());
        
        var clients = clientsGenerator.Generate(StaticVariables.ClientsGeneratorCount);
        dbContext.AddRangeAsync(clients);
        dbContext.SaveChangesAsync();

    }
}