using MediatR;

namespace TaskTracker.Application.Commands.Boards;

public class ArchiveBoardCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
