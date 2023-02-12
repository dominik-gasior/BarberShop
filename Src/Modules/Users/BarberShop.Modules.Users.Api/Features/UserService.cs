using BarberShop.Modules.Users.Api.Entities;
using BarberShop.Modules.Users.Api.Exceptions;
using BarberShop.Modules.Users.Api.Features.Command;
using BarberShop.Modules.Users.Api.Persistence;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Users.Api.Features;

internal interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(Guid id);
    Task<User> GetUserByNumberPhone(string numberPhone);
    Task<Guid> CreateNewUser(User user);
    Task<string> DeleteUser(Guid id);
    Task<string> UpdateUser(UpdateUserRequest user);
}
internal sealed class UserService : IUserService
{
    private readonly UsersDbContext _dbContext;
    private readonly IBus _bus;
    public UserService(UsersDbContext dbContext, IBus bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
        => await _dbContext.Users.ToListAsync();
    
    public async Task<User> GetUserById(Guid id)
    {
        var user = (await _dbContext
            .Users
            .FirstOrDefaultAsync(c => c.Id.Equals(id)))!;
        
        if (user is null) throw new UserNotFoundByIdException(id);
        
        return user;
    }
    public async Task<User> GetUserByNumberPhone(string numberPhone)
    {
        var user = (await _dbContext.Users.FirstOrDefaultAsync(c => c.NumberPhone.Equals(numberPhone)))!;
        if (user is null) throw new UserNotFoundByNumberPhoneException(numberPhone);
        return user;
    }

    public async Task<Guid> CreateNewUser(User user)
    {
        var isUser = await _dbContext.Users.FirstOrDefaultAsync(c => c.NumberPhone.Equals(user.NumberPhone));
        if (isUser is not null) throw new UserIsExistException(user.NumberPhone);
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        var isClient = Role.Klient == user.Role;
        await _bus.Publish
        (
            new UserCreated(user.Id, $"{user.FirstName} {user.LastName}", user.Email, user.NumberPhone, isClient)
        );
        return user.Id;
    }

    public async Task<string> DeleteUser(Guid id)
    {
        var user = await GetUserById(id);
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        var isClient = user.Role == Role.Klient;
        await _bus.Publish
        (
            new UserDeleted(user.Id, isClient)
        );
        return $"User #{id} was removed in database!";
    }

    public async Task<string> UpdateUser(UpdateUserRequest user)
    {
        var updateUser = await GetUserById(user.Id);
        
        var isFreeNumberPhone = await _dbContext.Users.AnyAsync(c => c.NumberPhone.Equals(user.NumberPhone));
        if (isFreeNumberPhone) throw new NumberPhoneisExistException(user.NumberPhone);
        
        updateUser.UpdateNumberPhone(user.NumberPhone);
        updateUser.UpdateEmail(user.Email);
        
        await _dbContext.SaveChangesAsync();
        
        var isClient = updateUser.Role == Role.Klient;
        await _bus.Publish
        (
            new UserUpdated(user.Id, user.NumberPhone, user.Email, isClient)
        );
        return $"Client #{user.Id} was updated in database";
    }
}