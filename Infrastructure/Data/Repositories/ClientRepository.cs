using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Src.Domain;

namespace Infrastructure.Data.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken ct);
    Task<Client> GetClientById(int id, CancellationToken ct);
    Task<Client> GetClientByNumberPhone(string numberPhone, CancellationToken ct);
    Task Insert(Client client, CancellationToken ct);
    Task Delete(Client client, CancellationToken ct);
}

internal class ClientRepository : IClientRepository
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken ct)
        => await _dbContext.Clients.ToListAsync(ct);

    public async Task<Client> GetClientById(int id, CancellationToken ct)
        => (await _dbContext
            .Clients
            .Include(c => c.Orders)
            .Include(c=> c.Visits)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: ct))!;

    

    public async Task<Client> GetClientByNumberPhone(string numberPhone, CancellationToken ct) 
        =>  (await _dbContext
            .Clients
            .Include(c => c.Orders)
            .Include(c => c.Visits)
            .FirstOrDefaultAsync(c => c.NumberPhone.Equals(numberPhone), cancellationToken: ct))!;
    

    public async Task Insert(Client client, CancellationToken ct)
        => await _dbContext.Clients.AddAsync(client, ct);

    public async Task Delete(Client client, CancellationToken ct)
        => await Task.FromResult(_dbContext.Clients.Remove(client));
}