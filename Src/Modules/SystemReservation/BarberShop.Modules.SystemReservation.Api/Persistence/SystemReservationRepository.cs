using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Persistence;

public interface ISystemReservationRepository
{
    Task<IEnumerable<Visit>> GetAllVisits();
    Task<Visit> GetVisitById(int id);
    Task<Visit> GetVisitByNumberPhone(string numberPhone);
    Task<IEnumerable<DateTime>> GetBusyTime(DateTime date);
    Task<Visit> IsFreeEmployee(Visit visit);
    Task Insert(Visit visit);
    Task Delete(Visit visit);
    Task SaveChangesAsync();
}

internal class SystemReservationRepository : ISystemReservationRepository
{
    private readonly SystemReservationDbContext _dbContext;

    public SystemReservationRepository(SystemReservationDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<Visit>> GetAllVisits()
        => await _dbContext.Visits.Include(v=>v.ServiceIndustry).ToListAsync();
    
    public async Task<Visit> GetVisitById(int id)
        => (await _dbContext
            .Visits
            .Include(v => v.ServiceIndustry)
            .FirstOrDefaultAsync(c => c.Id == id))!;

    public async Task<Visit> GetVisitByNumberPhone(string numberPhone)
        => (await _dbContext
            .Visits
            .Include(v => v.ServiceIndustry)
            .FirstOrDefaultAsync(c => c.NumberPhone.Equals(numberPhone)))!;

    public async Task<IEnumerable<DateTime>> GetBusyTime(DateTime date)
        => await _dbContext.Visits.Where(v => v.Date.Date == date.Date).Select(v=>v.Date).ToListAsync();

    public async Task<Visit> IsFreeEmployee(Visit visit)
        => (await _dbContext
            .Visits
            .FirstOrDefaultAsync(v => v.Date == visit.Date && v.EmployeeId == visit.EmployeeId))!;
    
    public async Task Insert(Visit visit)
        => await _dbContext.Visits.AddAsync(visit);

    public Task Delete(Visit visit)
        =>  Task.FromResult(_dbContext.Visits.Remove(visit));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}