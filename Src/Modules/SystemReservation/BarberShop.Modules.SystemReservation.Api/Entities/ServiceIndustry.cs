namespace BarberShop.Modules.SystemReservation.Api.Entities;

public sealed class ServiceIndustry
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Time { get; set; }

    public List<Visit> Visits { get; set; }
}