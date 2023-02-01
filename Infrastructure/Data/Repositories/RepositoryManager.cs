using Infrastructure.Data;
using Infrastructure.Data.Repositories;

namespace Src.Manager.RepositoryManager;

public interface IRepositoryManager
{
    IClientRepository ClientRepository { get; }
}

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IClientRepository> _lazyClientRepository;

    public RepositoryManager(AppDbContext dbContext)
    {
        _lazyClientRepository= new Lazy<IClientRepository>(() => new ClientRepository(dbContext));
    }

    public IClientRepository ClientRepository => _lazyClientRepository.Value;
}