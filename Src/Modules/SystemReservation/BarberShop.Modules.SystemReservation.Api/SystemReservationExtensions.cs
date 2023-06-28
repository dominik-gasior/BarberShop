using BarberShop.Modules.SystemReservation.Api.Features;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Modules.SystemReservation.Api.Persistence.Seeder;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.SystemReservation.Api;

public static class SystemReservationExtensions
{
    public static IServiceCollection AddSystemReservationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SystemReservationDbContext>(
            options =>
                options.UseSqlServer(configuration.GetConnectionString("dbConnString")));
        services.AddScoped<ISystemReservationService, SystemReservationService>();
        return services;
    }

    public static IApplicationBuilder UseSystemReservationModule(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        services.GetRequiredService<SystemReservationDbContext>().Database.Migrate();
        services.GetRequiredService<SystemReservationDbContext>().Seed();
        
        return app;
    }
}