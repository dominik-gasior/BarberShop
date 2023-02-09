using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Command;

public record CreateVisitRequest
{
    public string NumberPhone { get; set; }
    public int UserId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceIndustryId { get; set; }
    public DateTime Date { get; set; }
}

public class CreateVisitMapperProfile : RequestMapper<CreateVisitRequest, Visit>
{
    public override Visit ToEntity(CreateVisitRequest r)
        => new Visit
        {
            EmployeeId = r.EmployeeId,
            UserId = r.UserId,
            ServiceIndustryId = r.ServiceIndustryId,
            Date = r.Date,
            NumberPhone = r.NumberPhone,
        };
}

public class CreateVisitEndpoint : EndpointWithMapper<CreateVisitRequest,CreateVisitMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;
    public CreateVisitEndpoint(ISystemReservationService systemReservationService) =>
        _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Post("/api/createVisit");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateVisitRequest req, CancellationToken ct)
        => await _systemReservationService.CreateNewVisit(Map.ToEntity(req));
}