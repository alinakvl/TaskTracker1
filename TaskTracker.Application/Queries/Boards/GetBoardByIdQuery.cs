using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.Boards;

public class GetBoardByIdQuery : IRequest<BoardDetailDto?>
{
    public Guid Id { get; set; }
}
