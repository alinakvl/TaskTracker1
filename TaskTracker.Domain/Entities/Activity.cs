namespace TaskTracker.Domain.Entities;

public class Activity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }
    public Guid? BoardId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IPAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; } 
    public Board? Board { get; set; }
}

