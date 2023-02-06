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
    Task<string> UpdateUser(UpdateUserRequest user, CancellationToken ct);
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
        if (user is null) throw new UserNotFoundByIdException(id);
        return user;
    }
    public async Task<User> GetUserByNumberPhone(string numberPhone, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByNumberPhone(numberPhone, ct);
        if (user is null) throw new UserNotFoundByNumberPhoneException(numberPhone);
        return user;
    }

    public async Task<string> CreateNewUser(User user, CancellationToken ct)
    {
        var isUser = await _userRepository.GetUserByNumberPhone(user.NumberPhone, ct);
        if (isUser is not null) throw new UserIsExistException(user.Id);

        await _userRepository.Insert(user,ct);
        await _userRepository.SaveChangesAsync();
        return "User was created!";
    }

    public async Task<string> DeleteUser(int id, CancellationToken ct)
    {
        var user = await _userRepository.GetUserById(id, ct);

        if (user is null) throw new UserNotFoundByIdException(id);

        await _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();

        return $"User #{id} was removed in database!";
    }

    public async Task<string> UpdateUser(UpdateUserRequest user, CancellationToken ct)
    {
        if (user.NumberPhone.Equals("") && user.Email!.Equals("")) throw new NumberPhoneOrEmailEmptyException();
        
        var updateUser = await _userRepository.GetUserById(user.Id, ct);
        if (updateUser is null) throw new UserNotFoundByIdException(user.Id);

        if(!user.NumberPhone.Equals("")) updateUser.UpdateNumberPhone(user.NumberPhone);;
        if(!user.Email!.Equals("")) updateUser.UpdateEmail(user.Email);
        
        await _userRepository.SaveChangesAsync();
        return $"Client #{user.Id} was updated in database";
    }
}