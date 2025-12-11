using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.Boards.GetArchivedBoards;

public class GetArchivedBoardsQuery : IRequest<IEnumerable<BoardDto>>
{
}
