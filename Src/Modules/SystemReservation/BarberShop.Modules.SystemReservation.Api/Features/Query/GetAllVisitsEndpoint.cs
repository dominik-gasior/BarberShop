using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Query;

internal sealed record GetAllVisitsResponse(int Id, int UserId, int EmployeeId, string NameService, decimal Price, DateTime Date, string NumberPhone);

internal sealed class GetAllVisitsMapperProfile : ResponseMapper<IEnumerable<GetAllVisitsResponse>, IEnumerable<Visit>>
{
    public override IEnumerable<GetAllVisitsResponse> FromEntity(IEnumerable<Visit> e)
        => e.Select(v => new GetAllVisitsResponse(
                v.Id,
                v.ClientId,
                v.EmployeeId,
                v.ServiceIndustry.Name,
                v.ServiceIndustry.Price,
                v.Date,
                v.Client.NumberPhone
            )
        );
}

internal sealed class GetAllVisitsEndpoint : EndpointWithoutRequest<IEnumerable<GetAllVisitsResponse>, GetAllVisitsMapperProfile>
{
    private readonly ISystemReservationService _systemReservationService;

    public GetAllVisitsEndpoint(ISystemReservationService systemReservationService) =>
        _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Get("/api/getAllVisits");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _systemReservationService.GetAllVisits()), cancellation: ct);
}
