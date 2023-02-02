using Bogus;
using Infrastructure.Domain;

namespace Infrastructure.Data.Seeders;

internal static class ClientSeed
{
    internal static void SeedClients(AppDbContext dbContext, string locale)
    {
        if (dbContext.Clients.Any()) return;
        var clientsGenerator = new Faker<Client>(locale)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.NumberPhone, f => f.Random.Number(StaticVariables.MinNumberPhone, StaticVariables.MaxNumberPhone).ToString());
            
        var clients = clientsGenerator.Generate(StaticVariables.ClientsGeneratorCount);
            
        dbContext.AddRangeAsync(clients);
        dbContext.SaveChangesAsync();

    }
}