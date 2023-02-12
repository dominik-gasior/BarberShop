using Microsoft.IdentityModel.Tokens;

namespace BarberShop.Modules.Users.Api.Entities;

internal sealed class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NumberPhone { get; set; }
    public required string? Email { get; set; }
    public Role Role { get; set; }
    
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