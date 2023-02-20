namespace BarberShop.Modules.Warehouse.Api.Entities;

public sealed class Client
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string NumberPhone { get; set; }
    public required string? Email { get; set; }
    public List<Order> Orders { get; set; }
}