using MediatR;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Commands.BoardMembers.UpdateBoardMemberRole;

internal class UpdateBoardMemberRoleCommandHandler : IRequestHandler<UpdateBoardMemberRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBoardMemberRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateBoardMemberRoleCommand request, CancellationToken cancellationToken)
    {
        var boardMember = await _unitOfWork.BoardMembers.FirstOrDefaultAsync(
            bm => bm.BoardId == request.BoardId && bm.UserId == request.UserId, cancellationToken);

        if (boardMember == null) return false;

        boardMember.Role = request.Role;

        await _unitOfWork.BoardMembers.UpdateAsync(boardMember, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
