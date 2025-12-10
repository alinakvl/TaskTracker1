namespace TaskTracker.Domain.DTOs.Tasks;
using TaskTracker.Domain.DTOs.Comments;
using TaskTracker.Domain.DTOs.Attachments;

public class TaskDetailDto : TaskDto
{
    public List<CommentDto> Comments { get; set; } = new();
    public List<AttachmentDto> Attachments { get; set; } = new();
}
