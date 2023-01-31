using Src.Data;
using Src.Data.Repositories;
using Src.Features.ClientFeatures;

namespace Src.Manager.RepositoryManager;

public interface IRepositoryManager
{
    IClientRepository ClientRepository { get; }
}

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IClientRepository> _lazyClientRepository;

    public RepositoryManager(AppDbContext dbContext)
    {
        _lazyClientRepository= new Lazy<IClientRepository>(() => new ClientRepository(dbContext));
    }

    public IClientRepository ClientRepository => _lazyClientRepository.Value;
}