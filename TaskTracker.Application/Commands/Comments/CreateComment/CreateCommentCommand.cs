using MediatR;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Application.Commands.Comments.CreateComment;

public class CreateCommentCommand : IRequest<CommentDto>
{
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    public string Content { get; set; } = string.Empty;
}
