namespace BarberShop.Modules.SystemReservation.Api.Entities;

public sealed class Employee
{
    public required int Id { get; set; }
    public required string Fullname { get; set; }

    public List<Visit> Visits { get; set; }
}