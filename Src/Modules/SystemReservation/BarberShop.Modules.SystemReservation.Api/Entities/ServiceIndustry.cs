namespace BarberShop.Modules.SystemReservation.Api.Entities;

public class ServiceIndustry
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Time { get; set; }

    public List<Visit> Visits { get; set; }
}