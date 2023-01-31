using Src.Domain;
using Src.Manager.RepositoryManager;

namespace Src.Features.ClientFeatures;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken ct);
    Task<Client> GetClientById(int id, CancellationToken ct);
}
public class ClientService : IClientService
{
    private readonly IRepositoryManager _repositoryManager;

    public ClientService(IRepositoryManager repositoryManager)
        => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken ct)
        =>  await _repositoryManager.ClientRepository.GetAllClients(ct);

    public async Task<Client> GetClientById(int id, CancellationToken ct)
        => await _repositoryManager.ClientRepository.GetClientById(id, ct);
}