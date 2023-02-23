using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Query.Product;

internal sealed record GetAllProductsResponse(int Id, string Name, string Description, decimal Price, decimal LastPrice, int Amount);

internal sealed class GetAllProductsMapperProfile : ResponseMapper<IEnumerable<GetAllProductsResponse>, IEnumerable<Entities.Product>>
{
    public override IEnumerable<GetAllProductsResponse> FromEntity(IEnumerable<Entities.Product> e)
        => e.Select(p => new GetAllProductsResponse
            (
                p.Id, 
                p.Name,
                p.Description,
                p.Price,
                p.LastPrice,
                p.Amount
            ));
}

internal sealed class GetAllProductsEndpoint : EndpointWithoutRequest<IEnumerable<GetAllProductsResponse>,GetAllProductsMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public GetAllProductsEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Get("/api/getAllProducts");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _warehouseService.GetAllProducts()), cancellation: ct);
}