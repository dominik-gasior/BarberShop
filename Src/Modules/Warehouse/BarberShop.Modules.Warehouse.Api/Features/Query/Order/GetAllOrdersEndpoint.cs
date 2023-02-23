using BarberShop.Modules.Warehouse.Api.Entities;
using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Query.Order;

internal sealed record GetAllOrdersResponse(Guid Id, Guid ClientId, string ClientFullname,string ClientNumberPhone, decimal Cost, DateTime DeliveryTime, OrderStatus OrderStatus);

internal sealed class GetAllOrdersMapperProfile : ResponseMapper<IEnumerable<GetAllOrdersResponse>, IEnumerable<Entities.Order>>
{
    public override IEnumerable<GetAllOrdersResponse> FromEntity(IEnumerable<Entities.Order> e)
        => e.Select(o => new GetAllOrdersResponse(
            o.Id,
            o.ClientId,
            o.Client.FullName,
            o.Client.NumberPhone,
            o.Cost,
            o.DeliveryTime,
            o.OrderStatus
        ));
}
internal sealed class GetAllOrdersEndpoint : EndpointWithoutRequest<IEnumerable<GetAllOrdersResponse>,GetAllOrdersMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public GetAllOrdersEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;
    

    public override void Configure()
    {
        Get("/api/getAllOrders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _warehouseService.GetAllOrders()), cancellation: ct);


}