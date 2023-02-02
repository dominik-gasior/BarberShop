using Application.Features.Exceptions;
using Infrastructure.Data.Repositories;
using Infrastructure.Domain.SystemReservation;

namespace Application.Features.SystemReservationFeatures.EmployeeFeatures;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct);
    Task<Employee> GetEmployeeById(int id, CancellationToken ct);
    Task<Employee> GetEmployeeByNumberPhone(string numberPhone, CancellationToken ct);
}

internal class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repositoryManager;
    public EmployeeService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;


    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct)
        => await _repositoryManager.EmployeeRepository.GetAllEmployees(ct);

    public async Task<Employee> GetEmployeeById(int id, CancellationToken ct)
    {
        var employee = await _repositoryManager.EmployeeRepository.GetEmployeeById(id, ct);
        if (employee is null) throw new BadRequestException("Not found employee in database!");
        return employee;
    }

    public async Task<Employee> GetEmployeeByNumberPhone(string numberPhone, CancellationToken ct)
    {
        var employee = await _repositoryManager.EmployeeRepository.GetEmployeeByNumberPhone(numberPhone, ct);
        if (employee is null) throw new BadRequestException("Not found employee in database!");
        return employee;
    }
}