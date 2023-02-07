using BarberShop.Modules.Users.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Users.Api.Persistence;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> GetUserByNumberPhone(string numberPhone);
    Task Insert(User user);
    Task Delete(User user);
    Task SaveChangesAsync();
}

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
        => _dbContext = dbContext;
    public async Task<IEnumerable<User>> GetAllUsers()
        => await _dbContext.Users.ToListAsync();
    public async Task<User> GetUserById(int id)
        => (await _dbContext
            .Users
            .FirstOrDefaultAsync(c => c.Id == id))!;
    public async Task<User> GetUserByNumberPhone(string numberPhone)
        => (await _dbContext.Users.FirstOrDefaultAsync(c => c.NumberPhone.Equals(numberPhone)))!;
    public async Task Insert(User user)
        => await _dbContext.Users.AddAsync(user);

    public async Task Delete(User user)
        => await Task.FromResult(_dbContext.Users.Remove(user));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}