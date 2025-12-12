using MediatR;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Commands.BoardMembers.RemoveBoardMember;

internal class RemoveBoardMemberCommandHandler : IRequestHandler<RemoveBoardMemberCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveBoardMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveBoardMemberCommand request, CancellationToken cancellationToken)
    {
        var boardMember = await _unitOfWork.BoardMembers.FirstOrDefaultAsync(
            bm => bm.BoardId == request.BoardId && bm.UserId == request.UserId, cancellationToken);

        if (boardMember == null) return false;

        await _unitOfWork.BoardMembers.DeleteAsync(boardMember, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}