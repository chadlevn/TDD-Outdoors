namespace Domain.Models;

public class Activity
{
    public int ActivityId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
}