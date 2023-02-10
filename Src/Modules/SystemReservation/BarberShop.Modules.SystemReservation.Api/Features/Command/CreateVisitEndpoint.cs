using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Command;

internal sealed record CreateVisitRequest
{
    public string NumberPhone { get; set; }
    public int UserId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceIndustryId { get; set; }
    public DateTime Date { get; set; }
}

internal sealed class CreateVisitMapperProfile : RequestMapper<CreateVisitRequest, Visit>
{
    public override Visit ToEntity(CreateVisitRequest r)
        => new Visit
        {
            EmployeeId = r.EmployeeId,
            ClientId = r.UserId,
            ServiceIndustryId = r.ServiceIndustryId,
            Date = r.Date,
        };
}

internal sealed class CreateVisitEndpoint : EndpointWithMapper<CreateVisitRequest,CreateVisitMapperProfile>
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