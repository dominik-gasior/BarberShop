namespace Infrastructure.Data.Repositories;

public interface IRepositoryManager
{
    IClientRepository ClientRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IUnitOfWork UnitOfWork { get; }
}

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IClientRepository> _lazyClientRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<IEmployeeRepository> _lazyEmployeeRepository;

    public RepositoryManager(AppDbContext dbContext)
    {
        _lazyClientRepository= new Lazy<IClientRepository>(() => new ClientRepository(dbContext));
        _lazyEmployeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));
    }

    public IClientRepository ClientRepository => _lazyClientRepository.Value;
    public IEmployeeRepository EmployeeRepository => _lazyEmployeeRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}