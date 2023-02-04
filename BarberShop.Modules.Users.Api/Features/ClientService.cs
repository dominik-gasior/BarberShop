using BarberShop.Modules.Users.Api.Entities;
using BarberShop.Modules.Users.Api.Exceptions;
using BarberShop.Modules.Users.Api.Features.Command;
using BarberShop.Modules.Users.Api.Persistence;

namespace BarberShop.Modules.Users.Api.Features;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers(CancellationToken ct);
    Task<User> GetUserById(int id, CancellationToken ct);
    Task<User> GetUserByNumberPhone(string numberPhone, CancellationToken ct);
    Task<string> CreateNewUser(User user, CancellationToken ct);
    Task<string> DeleteUser(int id, CancellationToken ct);
    Task<string> UpdateUser(UpdateClientRequest user, CancellationToken ct);
}
internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<IEnumerable<User>> GetAllUsers(CancellationToken ct)
        => await _userRepository.GetAllUsers(ct);
    
    public async Task<User> GetUserById(int id, CancellationToken ct)
    {
        var user = await _userRepository.GetUserById(id, ct);
        if (user is null) throw new NotFoundExceptions("Not found user in database");
        return user;
    }
    public async Task<User> GetUserByNumberPhone(string numberPhone, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByNumberPhone(numberPhone, ct);
        if (user is null) throw new NotFoundExceptions("Not found client in database");
        return user;
    }

    public async Task<string> CreateNewUser(User user, CancellationToken ct)
    {
        var isUser = await _userRepository.GetUserByNumberPhone(user.NumberPhone, ct);
        if (isUser is not null) throw new BadRequestException($"User #{user.Id} is exist in database");

        await _userRepository.Insert(user,ct);
        await _userRepository.SaveChangesAsync();
        return "User was created!";
    }

    public async Task<string> DeleteUser(int id, CancellationToken ct)
    {
        var user = await _userRepository.GetUserById(id, ct);
        
        if (user is null) throw new BadRequestException("Not found user in database");

        await _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();

        return $"User #{id} was removed in database!";
    }

    public async Task<string> UpdateUser(UpdateClientRequest user, CancellationToken ct)
    {
        if (user.NumberPhone.Equals("") && user.Email!.Equals("")) throw new BadRequestException("Email or number phone are empty!");
        
        var updateUser = await _userRepository.GetUserById(user.Id, ct);
        if (updateUser is null) throw new NotFoundExceptions("Not found user in database");

        if(!user.NumberPhone.Equals("")) updateUser.UpdateNumberPhone(user.NumberPhone);;
        if(!user.Email!.Equals("")) updateUser.UpdateEmail(user.Email);
        
        await _userRepository.SaveChangesAsync();
        return $"Client #{user.Id} was updated in database";
    }
}