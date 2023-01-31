using Microsoft.EntityFrameworkCore;
using Src.Data;

namespace Src.Features.Client;


public interface IClientRepository
{
    Task<List<Domain.Client>> GetAllClients(CancellationToken cancellationToken);
}

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Domain.Client>> GetAllClients(CancellationToken cancellationToken)
    {
        var clients = await _dbContext.Clients.ToListAsync(cancellationToken);

        return clients;
    }
}