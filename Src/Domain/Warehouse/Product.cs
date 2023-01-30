namespace Src.Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int PriceProductId { get; set; }
    public int StockStatusId { get; set; }

    public PriceProduct PriceProduct { get; set; }
    public StockStatus StockStatus { get; set; }
    public List<Order> Orders { get; set; }
}