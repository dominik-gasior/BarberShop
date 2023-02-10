using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Query;

public record GetBusyTimeRequest{ public DateTime Date { get; init; }}

public class GetBusyTimeEndpoint : Endpoint<GetBusyTimeRequest>
{
    private readonly ISystemReservationService _systemReservationService;

    public GetBusyTimeEndpoint(ISystemReservationService systemReservationService) =>
        _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Get("api/busyTime");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBusyTimeRequest req, CancellationToken ct)
        => await SendAsync(await _systemReservationService.GetBusyTime(req.Date), cancellation: ct);
}