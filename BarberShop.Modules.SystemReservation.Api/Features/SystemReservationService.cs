using BarberShop.Modules.SystemReservation.Api.Entities;
using BarberShop.Modules.SystemReservation.Api.Exceptions;
using BarberShop.Modules.SystemReservation.Api.Persistence;

namespace BarberShop.Modules.SystemReservation.Api.Features;


public interface ISystemReservationService
{
    Task<IEnumerable<Visit>> GetAllVisits(CancellationToken ct);
    Task<Visit> GetVisitById(int id, CancellationToken ct);
    Task<string> CreateNewVisit(Visit visit, DateTime date,CancellationToken ct);
    Task<string> DeleteVisit(int id, CancellationToken ct);
}

internal class SystemReservationService : ISystemReservationService
{
    private readonly ISystemReservationRepository _systemReservationRepository;

    public SystemReservationService(ISystemReservationRepository systemReservationRepository) =>
        _systemReservationRepository = systemReservationRepository;
 
    public async Task<IEnumerable<Visit>> GetAllVisits(CancellationToken ct)
        => await _systemReservationRepository.GetAllVisits(ct);

    public async Task<Visit> GetVisitById(int id, CancellationToken ct)
    {
        var visit = await _systemReservationRepository.GetVisitById(id, ct);
        if (visit is null) throw new NotFoundVisitByIdException(id);

        return visit;
    }

    public Task<string> CreateNewVisit(Visit visit, DateTime date,CancellationToken ct)
    {
        var isFreeEmployee = 
    }

    public async Task<string> DeleteVisit(int id, CancellationToken ct)
    {
        var visit = await GetVisitById(id, ct);
        await _systemReservationRepository.Delete(visit);
        await _systemReservationRepository.SaveChangesAsync();
        return $"Visit #{id} was removed in database!";
    }
}