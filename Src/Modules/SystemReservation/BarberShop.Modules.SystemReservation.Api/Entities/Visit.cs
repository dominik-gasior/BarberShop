namespace BarberShop.Modules.SystemReservation.Api.Entities;

internal sealed class Visit
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceIndustryId { get; set; }
    public DateTime Date { get; set; }
    
    public ServiceIndustry ServiceIndustry { get; set; }
    public Client Client { get; set; }
    public Employee Employee { get; set; }
}
