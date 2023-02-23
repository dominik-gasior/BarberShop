using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;
using MassTransit.Transports;

namespace BarberShop.Modules.SystemReservation.Api.Features.Query;

internal sealed record GetAllVisitsByClientIdRequest{ public Guid ClientId { get; set; }}

internal sealed record GetAllVisitsByClientRespone(Guid VisitId, DateTime Date, string ServiceName, decimal ServicePrice);

internal sealed class GetAllVisitsByClientMapperProfile : ResponseMapper<IEnumerable<GetAllVisitsByClientRespone>, IEnumerable<Visit>>, IRequestMapper
{
    public override IEnumerable<GetAllVisitsByClientRespone> FromEntity(IEnumerable<Visit> e)
        => e.Select(v => new GetAllVisitsByClientRespone
            (
                v.Id,
                v.Date,
                v.ServiceIndustry.Name,
                v.ServiceIndustry.Price
            ));
}

internal sealed class GetAllVisitsByClientIdEndpoint : EndpointWithMapper<GetAllVisitsByClientIdRequest, GetAllVisitsByClientMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;

    public GetAllVisitsByClientIdEndpoint(ISystemReservationService systemReservationService)
        => _systemReservationService = systemReservationService;
    

    public override void Configure()
    {
        Get("/api/{ClientId}/getAllVisits");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllVisitsByClientIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _systemReservationService.GetAllVisitsByClientId(req.ClientId)), cancellation: ct);
}