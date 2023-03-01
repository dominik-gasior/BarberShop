using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Command.Order;

internal sealed record ChangeStatusOrderRequest{ public Guid OrderId { get; set; }};

internal sealed class ChangeStatusOrderEndpoint : Endpoint<ChangeStatusOrderRequest,string>
{
    private readonly IWarehouseService _warehouseService;

    public ChangeStatusOrderEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Put("/api/changeStatusOrder/{OrderId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ChangeStatusOrderRequest req, CancellationToken ct)
        => await SendAsync(await _warehouseService.ChangeStatusOrder(req.OrderId), cancellation: ct);


}