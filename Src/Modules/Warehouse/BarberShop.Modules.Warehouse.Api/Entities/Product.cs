namespace BarberShop.Modules.Warehouse.Api.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PriceProductId { get; set; }
    public int AmountProductId { get; set; }

    public decimal Price { get; set; }
    public int Amount { get; set; }
    public List<Order> Orders { get; set; }
}