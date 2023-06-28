using BarberShop.Modules.Users.Api.Features;
using BarberShop.Modules.Users.Api.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.Users.Api;

public static class UsersExtensions
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("dbConnString")));
        services.AddScoped<IUserService, UserService>();

        return services;
    }
    public static IApplicationBuilder UseUsersModule(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        dataContext.Database.Migrate();
        return app;
    }
}