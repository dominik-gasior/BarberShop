using Infrastructure.Data.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;
using Src.Domain;

namespace Infrastructure.Data.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken ct);
    Task<Client> GetClientById(int id, CancellationToken ct);
}

internal class ClientRepository : IClientRepository
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken ct)
        => await _dbContext.Clients.ToListAsync(ct);

    public async Task<Client> GetClientById(int id, CancellationToken ct)
    {
        var client = await _dbContext
            .Clients
            .Include(c => c.Orders)
            .Include(c=> c.Visits)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: ct);
        
        if (client is null) throw new NotFoundExceptions("Not found client in database!");
        
        return client;
    }
}