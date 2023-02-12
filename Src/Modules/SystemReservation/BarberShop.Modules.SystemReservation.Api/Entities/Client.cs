namespace BarberShop.Modules.SystemReservation.Api.Entities;

public sealed class Client
{
    public required Guid Id { get; init; }
    public required string Fullname { get; init; }
    public required string? Email { get; init; }
    public required string NumberPhone { get; init; }

    public List<Visit> Visits { get; init; } = new();
}