namespace TaskTracker.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public string? AvatarUrl { get; set; }
    public string Role { get; set; } = "User";

    // Navigation properties
    public ICollection<Board> OwnedBoards { get; set; } = new List<Board>();
    public ICollection<BoardMember> BoardMemberships { get; set; } = new List<BoardMember>();
    public ICollection<Task> AssignedTasks { get; set; } = new List<Task>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
