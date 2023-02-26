using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Command.Product;

internal sealed record UpdateProductRequest
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public bool IsAvailable { get; set; }
}
internal sealed record UpdateProductResponse(string Message);

internal sealed class UpdateProductMapperProfile : RequestMapper<UpdateProductRequest, Entities.Product>
{
    public override Entities.Product ToEntity(UpdateProductRequest r)
        => new Entities.Product
        {
            Id = r.Id,
            Name = null,
            Description = null,
            Price = r.Price,
            LastPrice = 0,
            Amount = r.Amount,
            IsAvailable = r.IsAvailable
        };
}

internal sealed class UpdateProductEndpoint : Endpoint<UpdateProductRequest,UpdateProductResponse, UpdateProductMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public UpdateProductEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {
        Put("/api/updateProduct/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
        => await SendAsync(new UpdateProductResponse(await _warehouseService.UpdateProduct(Map.ToEntity(req))), cancellation: ct);
}