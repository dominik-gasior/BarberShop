using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Command.Order;

internal sealed record DeleteOrderRequest{ public Guid OrderId { get; set; }}
internal sealed class DeleteOrderEndpoint : Endpoint<DeleteOrderRequest, string>
{
    private readonly IWarehouseService _warehouseService;

    public DeleteOrderEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Delete("/api/deleteOrder/{OrderId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteOrderRequest req, CancellationToken ct)
        => await SendAsync(await _warehouseService.DeleteOrder(req.OrderId), cancellation: ct);
}