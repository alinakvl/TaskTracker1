using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.Boards;

public class GetAllBoardsQuery : IRequest<IEnumerable<BoardDto>>
{
}
