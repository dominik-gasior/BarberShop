using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Command.Product;

internal sealed record CreateProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal LastPrice { get; set; }
    public int Amount { get; set; }
    public bool IsAvailable { get; set; }
}

internal sealed class CreateProductMapperProfile : RequestMapper<CreateProductRequest, Entities.Product>
{
    public override Entities.Product ToEntity(CreateProductRequest r)
        => new Entities.Product
            {
                Name = r.Name,
                Description = r.Description,
                Price = r.Price,
                LastPrice = r.LastPrice,
                Amount = r.Amount,
                IsAvailable = r.IsAvailable
            };
}
internal sealed class CreateProductEndpoint : Endpoint<CreateProductRequest,int,CreateProductMapperProfile>
{
    private readonly IWarehouseService _warehouseService;

    public CreateProductEndpoint(IWarehouseService warehouseService)
        =>_warehouseService = warehouseService;

    public override void Configure()
    {
        Post("/api/createNewProduct");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
        => await SendAsync(await _warehouseService.CreateNewProduct(Map.ToEntity(req)), cancellation: ct);
}















