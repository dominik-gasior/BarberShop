using Application.Features.ClientFeatures;
using Application.Features.SystemReservationFeatures.EmployeeFeatures;
using Infrastructure.Data.Repositories;

namespace Application.ServiceManager;

public interface IServiceManager
{
    IClientService ClientService { get; }
    IEmployeeService EmployeeService { get; }
}
public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IClientService> _lazyClientService;
    private readonly Lazy<IEmployeeService> _lazyEmployeeService;
    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyClientService = new Lazy<IClientService>(()=> new ClientService(repositoryManager));
        _lazyEmployeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager));
    }
    public IClientService ClientService => _lazyClientService.Value;
    public IEmployeeService EmployeeService => _lazyEmployeeService.Value;
}