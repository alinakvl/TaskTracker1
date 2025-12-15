using MediatR;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Application.Queries.Comments.GetCommentById;

public class GetCommentByIdQuery : IRequest<CommentDto?>
{
    public Guid Id { get; set; }
}
