using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Query;

internal sealed record GetVisitByNumberPhoneRequest{ public string NumberPhone { get; set; }};

internal sealed record GetVisitByNumberPhoneResponse(Guid Id, string NumberPhone, string NameService, decimal Price, DateTime VisitTime, int EmployeeId);

internal sealed class GetVisitByNumberPhoneMapperProfile : ResponseMapper<GetVisitByNumberPhoneResponse, Visit>, IRequestMapper
{
    public override GetVisitByNumberPhoneResponse FromEntity(Visit e)
        => new GetVisitByNumberPhoneResponse
        (
            e.Id,
            e.Client.NumberPhone,
            e.ServiceIndustry.Name,
            e.ServiceIndustry.Price,
            e.Date,
            e.EmployeeId
        );
}
internal sealed class GetVisitByNumberPhoneEndpoint : EndpointWithMapper<GetVisitByNumberPhoneRequest, GetVisitByNumberPhoneMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;

    public GetVisitByNumberPhoneEndpoint(ISystemReservationService systemReservationService) =>
        _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Get("api/visit/numberPhone/{NumberPhone}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetVisitByNumberPhoneRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _systemReservationService.GetVisitByNumberPhone(req.NumberPhone)), cancellation: ct);
}