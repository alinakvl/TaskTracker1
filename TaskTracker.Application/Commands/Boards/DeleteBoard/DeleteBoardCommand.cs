using MediatR;

namespace TaskTracker.Application.Commands.Boards.DeleteBoard;

public class DeleteBoardCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}