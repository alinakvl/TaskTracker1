namespace TaskTracker.Domain.DTOs.Boards;
public class AddBoardMemberDto
{
    public Guid UserId { get; set; }
    public int Role { get; set; } = 3;
}
