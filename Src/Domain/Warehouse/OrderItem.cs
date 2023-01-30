namespace Src.Domain;

public class OrderItem
{
    public Order Order { get; set; }
    public int OrderId { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
}