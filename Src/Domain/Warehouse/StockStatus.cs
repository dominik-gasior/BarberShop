namespace Src.Domain;

public class StockStatus
{
    public int Id { get; set; }
    public int Amount { get; set; }

    public Product Product { get; set; }
}