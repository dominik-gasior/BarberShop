using BarberShop.Modules.SystemReservation.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Persistence;

public interface ISystemReservationRepository
{
    Task<IEnumerable<Visit>> GetAllVisits(CancellationToken ct);
    Task<Visit> GetVisitById(int id, CancellationToken ct);
    Task<bool> IsFreeEmployee(int idEmployee, DateTime date, CancellationToken ct);
    Task Insert(Visit visit, CancellationToken ct);
    Task Delete(Visit visit);
    Task SaveChangesAsync();
}

internal class SystemReservationRepository : ISystemReservationRepository
{
    private readonly SystemReservationDbContext _dbContext;

    public SystemReservationRepository(SystemReservationDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task<IEnumerable<Visit>> GetAllVisits(CancellationToken ct)
        => await _dbContext.Visits.ToListAsync(ct);
    
    public async Task<Visit> GetVisitById(int id, CancellationToken ct)
        => (await _dbContext
            .Visits
            .FirstOrDefaultAsync(c => c.Id == id, ct))!;

    public Task<bool> IsFreeEmployee(int idEmployee, DateTime date, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task Insert(Visit visit, CancellationToken ct)
        => await _dbContext.Visits.AddAsync(visit, ct);

    public Task Delete(Visit visit)
        =>  Task.FromResult(_dbContext.Visits.Remove(visit));

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}