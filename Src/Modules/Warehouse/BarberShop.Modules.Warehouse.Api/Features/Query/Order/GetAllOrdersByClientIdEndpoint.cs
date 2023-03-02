using BarberShop.Modules.Warehouse.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Query.Order;

internal sealed record GetAllOrdersByClientIdRequest{ public Guid ClientId { get; set; }}
internal sealed record GetAllOrdersByClientIdResponse(Guid OrderId, DateTime DeliveryTime, OrderStatus OrderStatus, decimal Cost);

internal sealed class GetAllOrdersByClientIdMapperProfile : ResponseMapper<IEnumerable<GetAllOrdersByClientIdResponse>, IEnumerable<Entities.Order>>, IRequestMapper
{
    public override IEnumerable<GetAllOrdersByClientIdResponse> FromEntity(IEnumerable<Entities.Order> e)
        => e.Select(o => new GetAllOrdersByClientIdResponse(
            o.Id,
            o.DeliveryTime,
            o.OrderStatus,
            o.Cost
           ));
}

internal sealed class GetAllOrdersByClientIdEndpoint : EndpointWithMapper<GetAllOrdersByClientIdRequest,GetAllOrdersByClientIdMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public GetAllOrdersByClientIdEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Get("/api/{ClientId}/getAllOrdersForClient");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllOrdersByClientIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _warehouseService.GetAllOrdersByClientId(req.ClientId)), cancellation: ct);
}