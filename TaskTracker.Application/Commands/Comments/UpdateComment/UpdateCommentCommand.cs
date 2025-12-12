using MediatR;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Application.Commands.Comments.UpdateComment;

public class UpdateCommentCommand : IRequest<CommentDto>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } 
    public string Content { get; set; } = string.Empty;
}
