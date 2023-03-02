namespace BarberShop.Modules.Warehouse.Api.Entities;

public sealed class OrderProduct
{
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}