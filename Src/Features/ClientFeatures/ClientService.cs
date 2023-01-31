using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Src.Data;
using Src.Domain;
using Src.Features.ClientFeatures.Exceptions;
using Src.Manager.RepositoryManager;

namespace Src.Features.ClientFeatures;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllClients(CancellationToken ct);
}
public class ClientService : IClientService
{
    private readonly IRepositoryManager _repositoryManager;

    public ClientService(IRepositoryManager repositoryManager)
        => _repositoryManager = repositoryManager;

    public async Task<IEnumerable<Client>> GetAllClients(CancellationToken ct)
        =>  await _repositoryManager.ClientRepository.GetAllClients(ct);
    
    }