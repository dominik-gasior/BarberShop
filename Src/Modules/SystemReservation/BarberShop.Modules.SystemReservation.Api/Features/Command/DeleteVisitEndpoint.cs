using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Command;

internal sealed record DeleteVisitRequest{ public int VisitId { get; init; }};

internal sealed class DeleteVisitEndpoint : Endpoint<DeleteVisitRequest>
{
    private readonly ISystemReservationService _systemReservationService;

    public DeleteVisitEndpoint(ISystemReservationService systemReservationService) =>
                _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Delete("api/deleteVisit/{VisitId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteVisitRequest req, CancellationToken ct)
        => await SendAsync(await _systemReservationService.DeleteVisit(req.VisitId), cancellation: ct);
}