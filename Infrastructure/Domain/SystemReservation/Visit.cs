namespace Infrastructure.Domain.SystemReservation;

public class Visit
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int VisitTimeId { get; set; }
    public int ClientId { get; set; }
    public int ServiceIndustryId { get; set; }

    public Employee Employee { get; set; }
    public List<VisitTime> VisitTimes { get; set; } = new List<VisitTime>();
    public Client Client { get; set; }
    public ServiceIndustry ServiceIndustry { get; set; }
}