namespace Src.Domain;

public class PriceProduct
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal LastPrice { get; set; }

    public Product Product { get; set; }
}