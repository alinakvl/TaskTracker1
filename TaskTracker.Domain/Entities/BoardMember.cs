namespace TaskTracker.Domain.Entities;
public class BoardMember
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }
    public Guid UserId { get; set; }
    public int Role { get; set; } = 3; // 1-Owner, 2-Admin, 3-Member
    public DateTime JoinedAt { get; set; }

   
    public Board Board { get; set; } = null!;
    public User User { get; set; } = null!;
}
