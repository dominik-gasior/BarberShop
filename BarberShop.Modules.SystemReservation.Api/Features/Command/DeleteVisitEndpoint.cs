using FastEndpoints;

namespace BarberShop.Modules.SystemReservation.Api.Features.Command;

public record DeleteVisitRequest{ public int Id { get; init; }};

public class DeleteVisitEndpoint : Endpoint<DeleteVisitRequest>
{
    private readonly ISystemReservationService _systemReservationService;

    public DeleteVisitEndpoint(ISystemReservationService systemReservationService) =>
                _systemReservationService = systemReservationService;

    public override void Configure()
    {
        Delete("api/deleteVisit/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteVisitRequest req, CancellationToken ct)
        => await SendAsync(await _systemReservationService.DeleteVisit(req.Id), cancellation: ct);
}