using BarberShop.Modules.SystemReservation.Api.Entities;
using BarberShop.Modules.SystemReservation.Api.Exceptions;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Features;


internal interface ISystemReservationService
{
    Task<IEnumerable<Visit>> GetAllVisits();
    Task<IEnumerable<string>> GetBusyTime(DateTime date); 
    Task<Visit> GetVisitById(Guid id);
    Task<Visit> GetVisitByNumberPhone(string numberPhone);
    Task<string> CreateNewVisit(Visit visit);
    Task<string> DeleteVisit(Guid id);
}

internal sealed class SystemReservationService : ISystemReservationService
{
    private readonly SystemReservationDbContext _dbContext;
    public SystemReservationService(SystemReservationDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Visit>> GetAllVisits() 
        => await _dbContext.Visits.Include(v=>v.ServiceIndustry).ToListAsync();

    public async Task<IEnumerable<string>> GetBusyTime(DateTime date)
    {
        var busyDates = await _dbContext.Visits.Where(v => v.Date.Date == date.Date).Select(v=>v.Date).ToListAsync();
        return busyDates.Select(c => c.ToShortTimeString());
    }
    public async Task<Visit> GetVisitById(Guid id)
    {
        var visit = (await _dbContext
            .Visits
            .Include(v => v.ServiceIndustry)
            .FirstOrDefaultAsync(c => c.Id == id))!;
        if (visit is null) throw new NotFoundVisitByIdException(id);

        return visit;
    }

    public async Task<Visit> GetVisitByNumberPhone(string numberPhone)
    {
        var visit =  (await _dbContext
            .Visits
            .Include(v => v.ServiceIndustry)
            .Include(v=>v.Client)
            .FirstOrDefaultAsync(c => c.Client.NumberPhone.Equals(numberPhone)))!;
        if (visit is null) throw new NotFoundVisitByNumberPhoneException(numberPhone);

        return visit;
    }

    public async Task<string> CreateNewVisit(Visit visit)
    {
        var isFree = (await _dbContext
            .Visits
            .FirstOrDefaultAsync(v => v.Date == visit.Date && v.EmployeeId == visit.EmployeeId))!;
        
        if (isFree is not null) throw new BusyVisitException();

        await _dbContext.Visits.AddAsync(visit);
        await _dbContext.SaveChangesAsync();
        return $"Visit #{visit.Id}was created in database!";
    }

    public async Task<string> DeleteVisit(Guid id)
    {
        var visit = await GetVisitById(id);
        _dbContext.Visits.Remove(visit);
        await _dbContext.SaveChangesAsync();
        return $"Visit #{id} was removed in database!";
    }
}