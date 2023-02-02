using Infrastructure.Domain.SystemReservation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct);
}

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;
    public EmployeeRepository(AppDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct)
        => await _dbContext.Employees.Include(e => e.Role).ToListAsync(ct);
}