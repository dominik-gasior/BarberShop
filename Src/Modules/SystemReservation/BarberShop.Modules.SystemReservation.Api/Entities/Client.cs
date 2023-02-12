using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Modules.SystemReservation.Api.Entities;

public sealed class Client
{
    public required Guid Id { get; init; }
    public required string Fullname { get; init; }
    public required string? Email { get; set; }
    public required string NumberPhone { get; set; }

    public List<Visit> Visits { get; init; } = new();

    public void UpdateEmail(string email)
    {
        if (!email.IsNullOrEmpty())
        {
            Email = email;
        }
    }
    public void UpdateNumberPhone(string numberPhone)
    {
        if (!numberPhone.IsNullOrEmpty())
        {
            NumberPhone = numberPhone;
        }
    }
}