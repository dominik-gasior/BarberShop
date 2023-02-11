namespace BarberShop.Modules.SystemReservation.Api.Entities;

public sealed class Employee
{
    public int Id { get; set; }
    public string Fullname { get; set; }

    public List<Visit> Visits { get; set; }
}