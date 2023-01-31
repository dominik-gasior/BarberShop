using Bogus;
using Microsoft.IdentityModel.Tokens;
using Src.Domain;

namespace Src.Data;

public static class Seeder
{
    public static void Seed(AppDbContext dbContext)
    {
        var locale = "pl";
        Randomizer.Seed = new Random(StaticVariables.Seed);
        var serviceIndustries = new List<ServiceIndustry>()
        {
            new ServiceIndustry
            {
                Name = "Strzyzenie włosów",
                Price = 30.00m,
                Time = 30,
            },
            new ServiceIndustry
            {
                Name = "Strzyzenie brody",
                Price = 25.00m,
                Time = 30,
            },
            new ServiceIndustry
            {
                Name = "Strzyzenie brody i włosów",
                Price = 50.00m,
                Time = 60,
            }
        };
        if (!dbContext.ServiceIndustries.Any())
        {
            dbContext.AddRangeAsync(serviceIndustries);
            dbContext.SaveChangesAsync();
        }
        var roles = new List<Role>()
        {
            new Role
            {
                Name = "Właściciel"
            },
            new Role
            {
                Name = "Sekretarka"
            },
            new Role
            {
                Name = "Pracownik"
            }
        };

        if (!dbContext.Roles.Any())
        {
            dbContext.AddRangeAsync(roles);
            dbContext.SaveChangesAsync();
        }
        
        var employeesGenerator = new Faker<Employee>(locale)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.NumberPhone, f => f.Random.Number(100000000, 999999999).ToString())
            .RuleFor(p => p.RoleId, 1);

        var clientsGenerator = new Faker<Client>(locale)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.NumberPhone, f => f.Random.Number(100000000, 999999999).ToString());

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

        if (!dbContext.PriceProducts.Any())
        {
            dbContext.AddRangeAsync(pricesProducts);
            dbContext.SaveChangesAsync();
        }
        

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

        if (!dbContext.AmountProducts.Any())
        {
            dbContext.AddRangeAsync(amountsProducts);
            dbContext.SaveChangesAsync();
        }
        
        
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

        if (!dbContext.Products.Any())
        {
            dbContext.AddRangeAsync(products);
            dbContext.SaveChangesAsync();
        }
        
        
        var employees = employeesGenerator.Generate(StaticVariables.EmployeesGeneratorCount);
        var clients = clientsGenerator.Generate(StaticVariables.ClientsGeneratorCount);

        if (!dbContext.Clients.Any())
        {
            dbContext.AddRangeAsync(clients);
            dbContext.SaveChangesAsync();
        }

        if (!dbContext.Employees.Any())
        {
            dbContext.AddRangeAsync(employees);
            dbContext.SaveChangesAsync();
        }

        
    }
}