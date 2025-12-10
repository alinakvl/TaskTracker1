using MediatR;
using TaskTracker.Application.Interfaces.Repositories;


namespace TaskTracker.Application.Commands.Boards.DeleteBoard;

internal class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBoardCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await _unitOfWork.Boards.GetByIdAsync(request.Id, cancellationToken);

        if (board == null)
            return false;

        board.IsDeleted = true;
        board.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Boards.UpdateAsync(board, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
