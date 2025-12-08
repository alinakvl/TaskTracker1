using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.Boards;

public class GetArchivedBoardsQuery : IRequest<IEnumerable<BoardDto>>
{
}
