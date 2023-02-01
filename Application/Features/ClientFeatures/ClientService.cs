using Application.Features.ClientFeatures.Command;
using Src.Domain;
using Src.Manager.RepositoryManager;

namespace Application.Features.ClientFeatures;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken ct);
    Task<Client> GetClientById(int id, CancellationToken ct);
    Task<Client> GetClientByNumberPhone(string numberPhone, CancellationToken ct);
}
internal class ClientService : IClientService
{
    private readonly IRepositoryManager _repositoryManager;

    public ClientService(IRepositoryManager repositoryManager)
        => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken ct)
        =>  await _repositoryManager.ClientRepository.GetAllClients(ct);

    public async Task<Client> GetClientById(int id, CancellationToken ct)
        => await _repositoryManager.ClientRepository.GetClientById(id, ct);

    public async Task<Client> GetClientByNumberPhone(string numberPhone, CancellationToken ct)
        => await _repositoryManager.ClientRepository.GetClientByNumberPhone(numberPhone, ct);
}