namespace TaskTracker.Domain.Entities;

public class TaskList : BaseEntity
{
    public Guid BoardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }

    public Board? Board { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}


