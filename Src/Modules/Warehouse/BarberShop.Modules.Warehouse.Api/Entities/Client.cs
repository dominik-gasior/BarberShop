namespace BarberShop.Modules.Warehouse.Api.Entities;

internal sealed class Client
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string NumberPhone { get; set; }
    public required string? Email { get; set; }
    public List<Order> Orders { get; set; }
}