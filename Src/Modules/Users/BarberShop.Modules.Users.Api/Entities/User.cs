namespace BarberShop.Modules.Users.Api.Entities;

public sealed class User
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NumberPhone { get; private set; }
    public string? Email { get; private set; }
    public int RoleId { get; private set; }
    public Role Role { get; set; }

    private User(){}

    public User(int id, string firstName, string lastName, string numberPhone, string? email, int roleId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        NumberPhone = numberPhone;
        Email = email;
        RoleId = roleId;
    }

    public void UpdateEmail(string email) => Email = email;
    public void UpdateNumberPhone(string numberPhone) => NumberPhone = numberPhone;

}