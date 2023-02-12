using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Query;

internal sealed record GetVisitByIdRequest{public Guid VisitId { get; set; }};

internal sealed record GetVisitByIdResponse(Guid Id, string NumberPhone, string NameService, decimal Price, DateTime VisitTime, Guid EmployeeId);

internal sealed class GetVisitByIdMapperProfile : ResponseMapper<GetVisitByIdResponse, Visit>, IRequestMapper
{
    public override GetVisitByIdResponse FromEntity(Visit e)
        => new GetVisitByIdResponse
        (
            e.Id,
            e.Client.NumberPhone,
            e.ServiceIndustry.Name,
            e.ServiceIndustry.Price,
            e.Date,
            e.EmployeeGuid
        );
}
internal sealed class GetVisitByIdEndpoint : EndpointWithMapper<GetVisitByIdRequest, GetVisitByIdMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;

    public GetVisitByIdEndpoint(ISystemReservationService systemReservationService) =>
        _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Get("api/visit/{VisitId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetVisitByIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _systemReservationService.GetVisitById(req.VisitId)), cancellation: ct);
}