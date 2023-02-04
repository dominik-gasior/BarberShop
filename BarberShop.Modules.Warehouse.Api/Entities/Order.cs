using Infrastructure.Domain.Warehouse;

namespace BarberShop.Modules.Warehouse.Api.Entities;

public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public decimal Cost { get; set; }
    public DateTime DeliveryTime { get; set; }

    public List<Product> Products { get; set; }
}