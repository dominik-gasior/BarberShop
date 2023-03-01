using BarberShop.Modules.Warehouse.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Command.Order;

internal sealed record CreateOrderRequest
{
    public Guid ClientId { get; set; }
    public DateTime DeliveryTime { get; set; }
    public List<int> OrderProducts { get; set; }
}
internal sealed class CreateOrderMapperProfile : RequestMapper<CreateOrderRequest, Entities.Order>
{
    public override Entities.Order ToEntity(CreateOrderRequest r)
        => new Entities.Order
        {
            ClientId = r.ClientId,
            Cost = 0,
            DeliveryTime = r.DeliveryTime,
            OrderStatus = OrderStatus.Realizacja,
        };
}
internal sealed class CreateOrderEndpoint : Endpoint<CreateOrderRequest,Guid,CreateOrderMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public CreateOrderEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Post("/api/createNewOrder");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
        => await SendAsync(await _warehouseService.CreateNewOrder(Map.ToEntity(req), req.OrderProducts), cancellation: ct);
}