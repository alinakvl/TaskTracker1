using MediatR;
using System.Text.Json.Serialization;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Commands.Boards.CreateBoard;

public class CreateBoardCommand : IRequest<BoardDto>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? BackgroundColor { get; set; }

    [JsonIgnore]
    public Guid UserId { get; set; }
}