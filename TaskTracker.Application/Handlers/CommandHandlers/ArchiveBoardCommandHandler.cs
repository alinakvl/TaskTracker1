using MediatR;
using TaskTracker.Application.Commands.Boards;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Handlers.CommandHandlers;

public class ArchiveBoardCommandHandler : IRequestHandler<ArchiveBoardCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ArchiveBoardCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ArchiveBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await _unitOfWork.Boards.GetByIdAsync(request.Id, cancellationToken);

        if (board == null)
            return false;

        board.IsArchived = true;
        board.ArchivedAt = DateTime.UtcNow;
        board.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Boards.UpdateAsync(board, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}