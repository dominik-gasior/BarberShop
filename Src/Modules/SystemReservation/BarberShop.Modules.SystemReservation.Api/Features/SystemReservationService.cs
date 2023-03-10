using BarberShop.Modules.SystemReservation.Api.Entities;
using BarberShop.Modules.SystemReservation.Api.Exceptions;
using BarberShop.Modules.SystemReservation.Api.Persistence;
using BarberShop.Modules.SystemReservation.Shared.Event;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Modules.SystemReservation.Api.Features;


internal interface ISystemReservationService
{
    Task<IEnumerable<Visit>> GetAllVisits();
    Task<IEnumerable<Visit>> GetAllVisitsByClientId(Guid id);
    Task<IEnumerable<Visit>> GetAllVisitsByEmployeeId(Guid id);
    Task<IEnumerable<ServiceIndustry>> GetAllService();
    Task<IEnumerable<string>> GetBusyTime(DateTime date); 
    Task<Visit> GetVisitById(Guid id);
    Task<Guid> CreateNewVisit(Visit visit);
    Task<string> DeleteVisit(Guid id);
}

internal sealed class SystemReservationService : ISystemReservationService
{
    private readonly SystemReservationDbContext _dbContext;
    private readonly IBus _bus;
    public SystemReservationService(SystemReservationDbContext dbContext, IBus bus)
    {
        _dbContext = dbContext;
        _bus = bus;
    }

    public async Task<IEnumerable<Visit>> GetAllVisits() 
        => await _dbContext
            .Visits
            .Include(v=>v.ServiceIndustry)
            .Include(v=>v.Client)
            .Include(v=>v.Employee)
            .ToListAsync();

    public async Task<IEnumerable<Visit>> GetAllVisitsByClientId(Guid id)
        => await _dbContext
            .Visits
            .Include(v => v.ServiceIndustry)
            .Where(v=>v.Client.Id.Equals(id))
            .ToListAsync();

    public async Task<IEnumerable<Visit>> GetAllVisitsByEmployeeId(Guid id)
        => await _dbContext
            .Visits
            .Include(v => v.ServiceIndustry)
            .Include(v=>v.Client)
            .Where(v=>v.Client.Id.Equals(id))
            .ToListAsync();

    public async Task<IEnumerable<ServiceIndustry>> GetAllService()
        => await _dbContext.ServiceIndustries.ToListAsync(); 

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
    public async Task<Guid> CreateNewVisit(Visit visit)
    {
        var isFree = (await _dbContext
            .Visits
            .FirstOrDefaultAsync(v => v.Date.Equals(visit.Date) && v.EmployeeGuid.Equals(visit.EmployeeGuid)))!;
        
        if (isFree is not null) throw new BusyVisitException();

        await _dbContext.Visits.AddAsync(visit);
        await _dbContext.SaveChangesAsync();
        
        var visitCreated = (await _dbContext
            .Visits
            .Include(v => v.Client)
            .FirstOrDefaultAsync(v => v.Id.Equals(visit.Id)))!;
        
        await _bus.Publish
            (
              new VisitCreated(visitCreated.Client.Fullname, visitCreated.Date)   
            );
        
        return visitCreated.Id;
    }

    public async Task<string> DeleteVisit(Guid id)
    {
        var visit = await GetVisitById(id);
        _dbContext.Visits.Remove(visit);
        await _dbContext.SaveChangesAsync();
        return $"Visit #{id} was removed in database!";
    }
}