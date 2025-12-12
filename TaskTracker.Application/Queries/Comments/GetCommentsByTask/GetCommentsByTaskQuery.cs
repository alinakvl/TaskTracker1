using MediatR;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Application.Queries.Comments.GetCommentsByTask;

public class GetCommentsByTaskQuery : IRequest<IEnumerable<CommentDto>>
{
    public Guid TaskId { get; set; }
}
