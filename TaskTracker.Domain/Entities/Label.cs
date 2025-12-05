namespace TaskTracker.Domain.Entities;

public class Label
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#61BD4F";
    public DateTime CreatedAt { get; set; }

    public Board Board { get; set; } = null!;
    public ICollection<TaskLabel> TaskLabels { get; set; } = new List<TaskLabel>();
}

