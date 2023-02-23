using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Query;

internal sealed record GetAllVisitsByEmployeeRequest { public Guid EmployeeId { get; set; }}
internal sealed record GetAllVisitsByEmployeeResponse(Guid VisitId, string ClientFullName, string ClientNumberPhone, DateTime Date, string ServiceName, decimal ServicePrice);

internal sealed class GetAllVisitsByEmployeeMapperProfile : ResponseMapper<IEnumerable<GetAllVisitsByEmployeeResponse>, IEnumerable<Visit>>, IRequestMapper
{
    public override IEnumerable<GetAllVisitsByEmployeeResponse> FromEntity(IEnumerable<Visit> e)
        => e.Select(v => new GetAllVisitsByEmployeeResponse
            (
                v.Id,
                v.Client.Fullname,
                v.Client.NumberPhone,
                v.Date,
                v.ServiceIndustry.Name,
                v.ServiceIndustry.Price
            ));
}
internal sealed class GetAllVisitsByEmployeeIdEndpoint : EndpointWithMapper<GetAllVisitsByEmployeeRequest,GetAllVisitsByEmployeeMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;

    public GetAllVisitsByEmployeeIdEndpoint(ISystemReservationService systemReservationService)
        => _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Get("/api/{EmployeeId}/getAllVisitsForEmployee");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllVisitsByEmployeeRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _systemReservationService.GetAllVisitsByEmployeeId(req.EmployeeId)), cancellation: ct);
}