namespace Src.Domain;

public class Order
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public decimal Cost { get; set; }
    public DateTime DeliveryTime { get; set; }

    public List<Product> Products { get; set; }
    public Client Client { get; set; }
}