using MediatR;

namespace TaskTracker.Application.Commands.Boards.ArchiveBoard;

public class ArchiveBoardCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
