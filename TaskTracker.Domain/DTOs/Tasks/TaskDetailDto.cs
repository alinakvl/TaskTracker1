using TaskTracker.Domain.DTOs.Comments;
using TaskTracker.Domain.DTOs.Attachments;

namespace TaskTracker.Domain.DTOs.Tasks;

public class TaskDetailDto : TaskDto
{
    public List<CommentDto> Comments { get; set; } = new();
    public List<AttachmentDto> Attachments { get; set; } = new();
}
