using BarberShop.Modules.Warehouse.Api.Persistence;
using BarberShop.Modules.Warehouse.Api.Persistence.Seeder;
using BarberShop.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.Warehouse.Api;

public static class WarehouseExtensions
{
    public static IServiceCollection AddWarehouseModule(this IServiceCollection services)
    {
        services.AddDbContext<WarehouseDbContext>(
            options => options.UseSqlServer(ConnectionString.ConnString));
        
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