using Infrastructure.Domain.SystemReservation;

namespace BarberShop.Modules.SystemReservation.Api.Entities;

public class Visit
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int VisitTimeId { get; set; }
    public int ClientId { get; set; }
    public int ServiceIndustryId { get; set; }

    public Employee Employee { get; set; }
    public List<VisitTime> VisitTimes { get; set; } = new List<VisitTime>();
    public ServiceIndustry ServiceIndustry { get; set; }
}