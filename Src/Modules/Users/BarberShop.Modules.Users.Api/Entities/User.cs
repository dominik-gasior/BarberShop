namespace BarberShop.Modules.Users.Api.Entities;

internal sealed class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NumberPhone { get; set; }
    public string? Email { get; set; }
    public Role Role { get; set; }
}