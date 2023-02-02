namespace Infrastructure.Domain.Warehouse;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PriceProductId { get; set; }
    public int AmountProductId { get; set; }

    public PriceProduct PriceProduct { get; set; }
    public AmountProduct AmountProduct { get; set; }
    public List<Order> Orders { get; set; }
}