using BarberShop.Modules.SystemReservation.Api.Entities;
using BarberShop.Modules.SystemReservation.Api.Exceptions;
using BarberShop.Modules.SystemReservation.Api.Persistence;

namespace BarberShop.Modules.SystemReservation.Api.Features;


public interface ISystemReservationService
{
    Task<IEnumerable<Visit>> GetAllVisits();
    Task<Visit> GetVisitById(int id);
    Task<string> CreateNewVisit(Visit visit);
    Task<string> DeleteVisit(int id);
}

internal class SystemReservationService : ISystemReservationService
{
    private readonly ISystemReservationRepository _systemReservationRepository;

    public SystemReservationService(ISystemReservationRepository systemReservationRepository) =>
        _systemReservationRepository = systemReservationRepository;
 
    public async Task<IEnumerable<Visit>> GetAllVisits()
        => await _systemReservationRepository.GetAllVisits();

    public async Task<Visit> GetVisitById(int id)
    {
        var visit = await _systemReservationRepository.GetVisitById(id);
        if (visit is null) throw new NotFoundVisitByIdException(id);

        return visit;
    }

    public async Task<string> CreateNewVisit(Visit visit)
    {
        var isFree = await _systemReservationRepository.IsFreeEmployee(visit);
        if (isFree is not null) throw new Exception();

        await _systemReservationRepository.Insert(visit);
        await _systemReservationRepository.SaveChangesAsync();
        return $"Visit #{visit.Id}was created in database!";
    }

    public async Task<string> DeleteVisit(int id)
    {
        var visit = await GetVisitById(id);
        await _systemReservationRepository.Delete(visit);
        await _systemReservationRepository.SaveChangesAsync();
        return $"Visit #{id} was removed in database!";
    }
}