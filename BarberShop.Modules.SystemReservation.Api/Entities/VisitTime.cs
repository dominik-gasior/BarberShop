using BarberShop.Modules.SystemReservation.Api.Entities;

namespace Infrastructure.Domain.SystemReservation;

public class VisitTime
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public List<Visit> Visits { get; set; } = new List<Visit>();
}