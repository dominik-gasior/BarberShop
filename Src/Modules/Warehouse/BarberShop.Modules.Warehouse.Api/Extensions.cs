using BarberShop.Modules.Warehouse.Api.Persistence;
using BarberShop.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.Warehouse.Api;

public static class Extensions
{
    public static IServiceCollection AddWarehouseModule(this IServiceCollection services)
    {
        services.AddDbContext<WarehouseDbContext>(
            options => options.UseSqlServer(ConnectionString.ConnString));
        
        return services;
    }
}