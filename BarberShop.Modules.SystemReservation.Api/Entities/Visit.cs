namespace BarberShop.Modules.SystemReservation.Api.Entities;

public class Visit
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EmployeeId { get; set; }
    public int ServiceIndustryId { get; set; }
    public DateTime Date { get; set; }
    public ServiceIndustry ServiceIndustry { get; set; }
}