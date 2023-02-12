namespace BarberShop.Modules.Warehouse.Api.Entities;

internal sealed class Order
{
    public Guid Id { get; set; }
    public required int ClientId { get; set; }
    public required decimal Cost { get; set; }
    public required DateTime DeliveryTime { get; set; }
    public required OrderStatus OrderStatus { get; set; }
    
    public List<Product> Products { get; set; }
    public Client Client { get; set; }
}