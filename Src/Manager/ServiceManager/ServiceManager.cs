using Src.Data;
using Src.Data.Repositories;
using Src.Features.ClientFeatures;

namespace Src.ServiceManager;

public interface IServiceManager
{
    IClientService ClientService { get; }
}
public class ServiceManager : IServiceManager
{
    private readonly Lazy<IClientService> _lazyClientService;
    public ServiceManager(AppDbContext dbContext)
    {
        _lazyClientService = new Lazy<IClientService>(()=> new ClientRepository(dbContext));
    }
    public IClientService ClientService => _lazyClientService.Value;
}