namespace BarberShop.Modules.Warehouse.Api.Entities;

public sealed class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required decimal LastPrice { get; set; }
    public required int Amount { get; set; }
    public required bool IsAvailable { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}