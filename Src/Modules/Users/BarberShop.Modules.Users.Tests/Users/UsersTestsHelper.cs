using BarberShop.Modules.Users.Api.Entities;
using BarberShop.Modules.Users.Api.Features.Command;
using BarberShop.Modules.Users.Api.Persistence;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShop.Modules.Users.Tests.Users;

internal static class UsersTestsHelper
{
    internal static async Task<User> Add(WebApplicationFactory<Program> apiFactory)
    {
        using var scope = apiFactory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        var user = new User
        {
            FirstName = "Adam",
            LastName = "Kowalski",
            NumberPhone = "123456789",
            Email = "a.kowalski@mail.com",
            Role = Role.Klient
        };
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public static async Task Delete(WebApplicationFactory<Program> apiFactory, Guid id)
    {
        using var scope = apiFactory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(u => u.Id.Equals(id));
            
        user.Should().NotBeNull();
        
        if (user is not null)
        {
            dbContext.Remove(user);
            await dbContext.SaveChangesAsync();
        }
    }
}