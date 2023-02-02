using Bogus;
using Infrastructure.Domain.SystemReservation;

namespace Infrastructure.Data.Seeders;

internal static class EmployeeSeed
{
    internal static void SeedEmployees(AppDbContext dbContext, string locale)
    {
        if (dbContext.Employees.Any()) return;
        var employeesGenerator = new Faker<Employee>(locale)
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.NumberPhone, f => f.Random.Number(StaticVariables.MinNumberPhone, StaticVariables.MaxNumberPhone).ToString())
            .RuleFor(p => p.RoleId, 1);
            
        var employees = employeesGenerator.Generate(StaticVariables.EmployeesGeneratorCount);
        dbContext.AddRangeAsync(employees);
        dbContext.SaveChangesAsync();

    }
}