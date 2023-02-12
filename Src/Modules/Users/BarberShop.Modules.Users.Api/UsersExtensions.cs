using BarberShop.Modules.Users.Api.Features;
using BarberShop.Modules.Users.Api.Persistence;
using BarberShop.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.Users.Api;

public static class UsersExtensions
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddDbContext<UsersDbContext>(
            options => options.UseSqlServer(ConnectionString.ConnString));
        services.AddScoped<IUserService, UserService>();

        return services;
    }
    public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
    {
        return app;
    }
}