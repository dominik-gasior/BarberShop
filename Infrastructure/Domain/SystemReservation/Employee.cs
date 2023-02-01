namespace Src.Domain;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string NumberPhone { get; set; }
    public int RoleId { get; set; }

    public Role Role { get; set; }
    public List<Visit> Visits { get; set; } = new List<Visit>();
}