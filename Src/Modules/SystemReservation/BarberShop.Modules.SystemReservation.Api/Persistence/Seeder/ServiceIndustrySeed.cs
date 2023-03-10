using BarberShop.Modules.SystemReservation.Api.Entities;

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Seeder;

internal static class ServiceIndustrySeed
{
    internal static void SeedServiceIndustries(SystemReservationDbContext dbContext)
    {
        if (dbContext.ServiceIndustries.Any()) return;
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
        dbContext.AddRangeAsync(serviceIndustries);
        dbContext.SaveChangesAsync();
    }
}