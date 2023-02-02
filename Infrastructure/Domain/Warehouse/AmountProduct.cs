namespace Infrastructure.Domain.Warehouse;

public class AmountProduct
{
    public int Id { get; set; }
    public int Amount { get; set; }

    public Product Product { get; set; }
}