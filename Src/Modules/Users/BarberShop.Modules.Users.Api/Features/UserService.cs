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
    Task<string> CreateNewUser(User user);
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
            .FirstOrDefaultAsync(c => c.Id == id))!;
        
        if (user is null) throw new UserNotFoundByIdException(id);
        
        return user;
    }
    public async Task<User> GetUserByNumberPhone(string numberPhone)
    {
        var user = (await _dbContext.Users.FirstOrDefaultAsync(c => c.NumberPhone.Equals(numberPhone)))!;
        if (user is null) throw new UserNotFoundByNumberPhoneException(numberPhone);
        return user;
    }

    public async Task<string> CreateNewUser(User user)
    {
        var isUser = await GetUserByNumberPhone(user.NumberPhone);
        if (isUser is not null) throw new UserIsExistException(user.NumberPhone);
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        
        await _bus.Publish
        (
            new UserCreated(user.Id, $"{user.FirstName} {user.LastName}", user.Email, user.NumberPhone)
        );
        return "User was created!";
    }

    public async Task<string> DeleteUser(Guid id)
    {
        var user = await GetUserById(id);

        if (user is null) throw new UserNotFoundByIdException(id);

         _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return $"User #{id} was removed in database!";
    }

    public async Task<string> UpdateUser(UpdateUserRequest user)
    {
        if (user.NumberPhone!.Equals("") && user.Email!.Equals("")) throw new NumberPhoneOrEmailEmptyException();
        
        var updateUser = await GetUserById(user.Id);
        if (updateUser is null) throw new UserNotFoundByIdException(user.Id);

        if(!user.NumberPhone.Equals("")) updateUser.NumberPhone = user.NumberPhone;
        if(!user.Email!.Equals("")) updateUser.NumberPhone = user.Email;
        
        await _dbContext.SaveChangesAsync();
        return $"Client #{user.Id} was updated in database";
    }
}