namespace BarberShop.Modules.Warehouse.Api.Entities;

internal sealed class Client
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string NumberPhone { get; set; }
    public string Email { get; set; }
    public List<Order> Orders { get; set; }
}