using BarberShop.Modules.SystemReservation.Api.Features;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Shared;
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
}