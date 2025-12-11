namespace TaskTracker.Domain.DTOs.Comments;

public class CreateCommentDto
{
    public Guid TaskId { get; set; }
    public string Content { get; set; } = string.Empty;
}
