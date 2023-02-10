namespace BarberShop.Modules.SystemReservation.Api.Entities;

internal sealed class Client
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string NumberPhone { get; set; }

    public List<Visit> Visits { get; set; }
}