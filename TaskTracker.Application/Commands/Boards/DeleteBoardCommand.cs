using MediatR;

namespace TaskTracker.Application.Commands.Boards;

public class DeleteBoardCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}