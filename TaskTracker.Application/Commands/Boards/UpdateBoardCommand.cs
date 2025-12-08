using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Commands.Boards;

public class UpdateBoardCommand : IRequest<BoardDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? BackgroundColor { get; set; }
}
