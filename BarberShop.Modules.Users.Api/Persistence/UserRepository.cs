using BarberShop.Modules.Users.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.Users.Api.Persistence;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers(CancellationToken ct);
    Task<User> GetUserById(int id, CancellationToken ct);
    Task<User> GetUserByNumberPhone(string numberPhone, CancellationToken ct);
    Task Insert(User user, CancellationToken ct);
    Task Delete(User user);
    Task SaveChangesAsync();
}

internal class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
        => _dbContext = dbContext;
    public async Task<IEnumerable<User>> GetAllUsers(CancellationToken ct)
        => await _dbContext.Users.ToListAsync(ct);
    public async Task<User> GetUserById(int id, CancellationToken ct)
        => (await _dbContext
            .Users
            .FirstOrDefaultAsync(c => c.Id == id, ct))!;
    public async Task<User> GetUserByNumberPhone(string numberPhone, CancellationToken ct)
        => (await _dbContext.Users.FirstOrDefaultAsync(c => c.NumberPhone.Equals(numberPhone), ct))!;
    public async Task Insert(User client, CancellationToken ct)
        => await _dbContext.Users.AddAsync(client, ct);

    public async Task Delete(User client)
        => await Task.FromResult(_dbContext.Users.Remove(client));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}