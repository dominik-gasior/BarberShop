using FastEndpoints;

namespace BarberShop.Modules.Warehouse.Api.Features.Command.Product;

internal sealed record DeleteProductRequest
{
    public int Id { get; set; }
}
internal sealed record DeleteProductResponse(string Message);
internal sealed class DeleteProductEndpoint : Endpoint<DeleteProductRequest,DeleteProductResponse>
{
    private readonly IWarehouseService _warehouseService;

    public DeleteProductEndpoint(IWarehouseService warehouseService)
        => _warehouseService = warehouseService;

    public override void Configure()
    {  
        Delete("/api/deleteProduct/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
        => await SendAsync(new DeleteProductResponse(await _warehouseService.DeleteProduct(req.Id)), cancellation: ct);
}