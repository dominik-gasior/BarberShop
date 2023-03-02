using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Query.Product;

internal sealed record GetAllProductByOrderIdRequest{ public Guid OrderId { get; set; }}
internal sealed record GetAllProductByOrderIdResponse(int Id, string Name, string Description, decimal Price, decimal LastPrice, bool IsAvailable,int AmountProductsInOrder);

internal sealed class GetAllProductByOrderIdMapperProfile : ResponseMapper<IEnumerable<GetAllProductByOrderIdResponse>, IEnumerable<Entities.Product>>, IRequestMapper
{
    public override IEnumerable<GetAllProductByOrderIdResponse> FromEntity(IEnumerable<Entities.Product> e)
        => e.Select(p=>new GetAllProductByOrderIdResponse
            (
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.LastPrice,
                p.IsAvailable,
                p.Amount
            ));
}
internal sealed class GetAllProductByOrderIdEndpoint : EndpointWithMapper<GetAllProductByOrderIdRequest,GetAllProductByOrderIdMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public GetAllProductByOrderIdEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Get("/api/{OrderId}/getAllProductsForOrder");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAllProductByOrderIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _warehouseService.GetAllProductsByOrderId(req.OrderId)), cancellation: ct);
}