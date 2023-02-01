using Application.Features.ClientFeatures;
using Src.Features.ClientFeatures;
using Src.Manager.RepositoryManager;

namespace Src.Manager.ServiceManager;

public interface IServiceManager
{
    IClientService ClientService { get; }
}
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IClientService> _lazyClientService;
    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyClientService = new Lazy<IClientService>(()=> new ClientService(repositoryManager));
    }
    public IClientService ClientService => _lazyClientService.Value;
}