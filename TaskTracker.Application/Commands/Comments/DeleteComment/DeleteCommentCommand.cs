using MediatR;

namespace TaskTracker.Application.Commands.Comments.DeleteComment;

public class DeleteCommentCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } 
}
