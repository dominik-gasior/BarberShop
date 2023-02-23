using FastEndpoints;
using MassTransit.Transports;

namespace BarberShop.Modules.Warehouse.Api.Features.Query.Product;

internal sealed record GetProductByIdRequest{ public int Id { get; set; }}
internal sealed record GetProductByIdResponse(int Id, string Name, string Description, decimal Price, decimal LastPrice, int Amount);

internal sealed class GetProductByIdMapperProfile : ResponseMapper<GetProductByIdResponse, Entities.Product>, IRequestMapper
{
    public override GetProductByIdResponse FromEntity(Entities.Product e)
        => new GetProductByIdResponse
            (
                e.Id,
                e.Name,
                e.Description,
                e.Price,
                e.LastPrice,
                e.Amount
            );
}
internal sealed class GetProductByIdEndpoint : EndpointWithMapper<GetProductByIdRequest, GetProductByIdMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public GetProductByIdEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Get("/api/getProduct/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductByIdRequest req, CancellationToken ct)
        => await SendAsync(Map.FromEntity(await _warehouseService.GetProductById(req.Id)), cancellation: ct);
}