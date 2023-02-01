namespace Src.Domain;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NumberPhone { get; set; }
    public string? Email { get; set; }

    public List<Visit> Visits { get; set; }
    public List<Order> Orders { get; set; }
}