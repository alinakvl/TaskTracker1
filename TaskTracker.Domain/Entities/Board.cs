namespace TaskTracker.Domain.Entities;

public class Board : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }
    public string? BackgroundColor { get; set; } = "#0079BF";
    public bool IsArchived { get; set; }
    public DateTime? ArchivedAt { get; set; }

    
   
    public User? Owner { get; set; }
    public ICollection<BoardMember> Members { get; set; } = new List<BoardMember>();
    public ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();
    public ICollection<Label> Labels { get; set; } = new List<Label>();
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}

