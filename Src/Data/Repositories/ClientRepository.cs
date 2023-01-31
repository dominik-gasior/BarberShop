using Microsoft.EntityFrameworkCore;
using Src.Domain;
using Src.Features.ClientFeatures;

namespace Src.Data.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients();
}

public class ClientRepository : IClientService
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken ct)
        => await _dbContext.Clients.ToListAsync(ct);
}