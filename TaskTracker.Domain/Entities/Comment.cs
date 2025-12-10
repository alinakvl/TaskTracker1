namespace TaskTracker.Domain.Entities;

public class Comment : BaseEntity
{
    public Guid TaskId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; } = string.Empty;

    public Task? Task { get; set; } 
    public User? User { get; set; }
}
