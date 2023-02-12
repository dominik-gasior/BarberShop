namespace BarberShop.Modules.SystemReservation.Api.Entities;

public sealed class Visit
{
    public Guid Id { get; set; }
    public required int ClientId { get; set; }
    public required int EmployeeId { get; set; }
    public required int ServiceIndustryId { get; set; }
    public required DateTime Date { get; set; }
    
    public ServiceIndustry ServiceIndustry { get; set; }
    public Client Client { get; set; }
    public Employee Employee { get; set; }
}
