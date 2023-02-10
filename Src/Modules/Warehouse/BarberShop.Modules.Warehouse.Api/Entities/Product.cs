namespace BarberShop.Modules.Warehouse.Api.Entities;

internal sealed class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal LastPrice { get; set; }
    public int Amount { get; set; }
    public List<Order> Orders { get; set; }
}