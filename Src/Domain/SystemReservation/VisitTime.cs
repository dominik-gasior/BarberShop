namespace Src.Domain;

public class VisitTime
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public List<Visit> Visits { get; set; } = new List<Visit>();
}