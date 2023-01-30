namespace Src.Domain;

public class FreeTime
{
    public VisitTime VisitTime { get; set; }
    public int VisitTimeId { get; set; }
    public Visit Visit { get; set; }
    public int VisitId { get; set; }
}