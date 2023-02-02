using Infrastructure.Domain.SystemReservation;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct);
    Task<Employee> GetEmployeeById(int id, CancellationToken ct, bool include = true);
    Task<Employee> GetEmployeeByNumberPhone(string numberPhone, CancellationToken ct, bool include = true);
}

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;
    public EmployeeRepository(AppDbContext dbContext) => _dbContext = dbContext;


    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct)
        => await _dbContext.Employees.Include(e => e.Role).ToListAsync(ct);

    public async Task<Employee> GetEmployeeById(int id, CancellationToken ct, bool include = true)
    {
        if (include)
        {
            return (await _dbContext
                .Employees
                .Include(e => e.Role)
                .Include(e => e.Visits)
                .FirstOrDefaultAsync(e => e.Id == id, ct))!;
        }
        return (await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id, ct))!;
    }

    public async Task<Employee> GetEmployeeByNumberPhone(string numberPhone, CancellationToken ct, bool include = true)
    {
        if (include)
        {
            return (await _dbContext
                .Employees
                .Include(e => e.Role)
                .Include(e => e.Visits)
                .FirstOrDefaultAsync(e => e.NumberPhone.Equals(numberPhone), ct))!;
        }
        return (await _dbContext.Employees.FirstOrDefaultAsync(e => e.NumberPhone.Equals(numberPhone), ct))!;
    }
}