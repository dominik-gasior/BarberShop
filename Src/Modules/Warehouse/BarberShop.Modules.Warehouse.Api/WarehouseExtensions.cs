using BarberShop.Modules.Warehouse.Api.Features;
using BarberShop.Modules.Warehouse.Api.Persistence;
using BarberShop.Modules.Warehouse.Api.Persistence.Seeder;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.Warehouse.Api;

public static class WarehouseExtensions
{
    public static IServiceCollection AddWarehouseModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarehouseDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("dbConnString")));
        services.AddScoped<IWarehouseService, WarehouseService>();
        return services;
    }
    public static IApplicationBuilder UseWarehouseModule(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        services.GetRequiredService<WarehouseDbContext>().Seed();
        return app;
    }
}