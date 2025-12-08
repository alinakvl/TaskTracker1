using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.Boards;

public class GetBoardsByUserIdQuery : IRequest<IEnumerable<BoardDto>>
{
    public Guid UserId { get; set; }
}
