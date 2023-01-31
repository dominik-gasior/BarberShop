using Src.Data;
using Src.Data.Repositories;
using Src.Features.ClientFeatures;

namespace Src.Manager.RepositoryManager;

public interface IRepositoryManager
{
    IClientService ClientService { get; }
}

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IClientService> _lazyClientServices;

    public RepositoryManager(AppDbContext dbContext)
    {
        _lazyClientServices = new Lazy<IClientService>(() => new ClientRepository(dbContext));
    }

    public IClientService ClientService => _lazyClientServices.Value;
}