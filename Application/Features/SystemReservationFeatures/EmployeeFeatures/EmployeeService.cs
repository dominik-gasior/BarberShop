using Infrastructure.Data.Repositories;
using Infrastructure.Domain.SystemReservation;

namespace Application.Features.SystemReservationFeatures.EmployeeFeatures;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct);
}

internal class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repositoryManager;
    public EmployeeService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;


    public Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct)
        => _repositoryManager.EmployeeRepository.GetAllEmployees(ct);
}