using BarberShop.Modules.Users.Api.Entities;
using BarberShop.Modules.Users.Api.Exceptions;
using BarberShop.Modules.Users.Api.Features.Command;
using BarberShop.Modules.Users.Api.Persistence;
using BarberShop.Modules.Users.Shared.Event;
using MassTransit;
using RabbitMQ.Client.Logging;

namespace BarberShop.Modules.Users.Api.Features;

internal interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> GetUserByNumberPhone(string numberPhone);
    Task<string> CreateNewUser(User user);
    Task<string> DeleteUser(int id);
    Task<string> UpdateUser(UpdateUserRequest user);
}
internal sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IBus _bus;
    public UserService(IUserRepository userRepository, IBus bus)
    {
        _userRepository = userRepository;
        _bus = bus;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
        => await _userRepository.GetAllUsers();
    
    public async Task<User> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user is null) throw new UserNotFoundByIdException(id);
        return user;
    }
    public async Task<User> GetUserByNumberPhone(string numberPhone)
    {
        var user = await _userRepository.GetUserByNumberPhone(numberPhone);
        if (user is null) throw new UserNotFoundByNumberPhoneException(numberPhone);
        return user;
    }

    public async Task<string> CreateNewUser(User user)
    {
        var isUser = await _userRepository.GetUserByNumberPhone(user.NumberPhone);
        if (isUser is not null) throw new UserIsExistException(user.NumberPhone);
        
        await _userRepository.Insert(user);
        await _userRepository.SaveChangesAsync();
        
        await _bus.Publish
        (
            new UserCreated(user.Id, $"{user.FirstName} {user.LastName}", user.Email, user.NumberPhone)
        );
        return "User was created!";
    }

    public async Task<string> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserById(id);

        if (user is null) throw new UserNotFoundByIdException(id);

        await _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();

        return $"User #{id} was removed in database!";
    }

    public async Task<string> UpdateUser(UpdateUserRequest user)
    {
        if (user.NumberPhone!.Equals("") && user.Email!.Equals("")) throw new NumberPhoneOrEmailEmptyException();
        
        var updateUser = await _userRepository.GetUserById(user.Id);
        if (updateUser is null) throw new UserNotFoundByIdException(user.Id);

        if(!user.NumberPhone.Equals("")) updateUser.NumberPhone = user.NumberPhone;
        if(!user.Email!.Equals("")) updateUser.NumberPhone = user.Email;
        
        await _userRepository.SaveChangesAsync();
        return $"Client #{user.Id} was updated in database";
    }
}