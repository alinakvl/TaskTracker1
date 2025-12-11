namespace TaskTracker.Domain.Entities;

public class Task : BaseEntity
{
    public Guid ListId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? AssignedUserId { get; set; }
    public int Position { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; } = 2;
    public TaskList? List { get; set; } 
    public User? AssignedUser { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    public ICollection<TaskLabel> TaskLabels { get; set; } = new List<TaskLabel>();
}

