namespace BarberShop.Modules.SystemReservation.Api.Entities;

public class VisitTime
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public List<Visit> Visits { get; set; } = new List<Visit>();
}