using BarberShop.Modules.SystemReservation.Api.Features;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Modules.SystemReservation.Api.Persistence.Seeder;
using BarberShop.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.SystemReservation.Api;

public static class Extensions
{
    public static IServiceCollection AddSystemReservationModule(this IServiceCollection services)
    {
        services.AddDbContext<SystemReservationDbContext>(
            options =>
                options.UseSqlServer(ConnectionString.ConnString));
        services.AddScoped<ISystemReservationRepository, SystemReservationRepository>();
        services.AddScoped<ISystemReservationService, SystemReservationService>();
        return services;
    }

    public static IApplicationBuilder UseSystemReservationModule(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        services.GetRequiredService<SystemReservationDbContext>().Seed();
        return app;
    }
}