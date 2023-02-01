namespace Infrastructure.Data.Repositories;

public interface IRepositoryManager
{
    IClientRepository ClientRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IClientRepository> _lazyClientRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public RepositoryManager(AppDbContext dbContext)
    {
        _lazyClientRepository= new Lazy<IClientRepository>(() => new ClientRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
    }

    public IClientRepository ClientRepository => _lazyClientRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}