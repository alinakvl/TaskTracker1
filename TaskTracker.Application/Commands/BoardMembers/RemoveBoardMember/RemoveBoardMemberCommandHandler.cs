using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Constants;

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
        var targetMember = await _unitOfWork.BoardMembers.FirstOrDefaultAsync( 
            bm => bm.BoardId == request.BoardId && bm.UserId == request.TargetUserId, cancellationToken);

        if (targetMember == null) return false; 

        var requesterMember = await _unitOfWork.BoardMembers.FirstOrDefaultAsync( 
            bm => bm.BoardId == request.BoardId && bm.UserId == request.CurrentUserId, cancellationToken);

        if (requesterMember == null)
        {
            throw new UnauthorizedAccessException("You are not a member of this board."); 
        }


        if (targetMember.Role == BoardMemberRoles.Owner)
        {
            throw new UnauthorizedAccessException("Cannot remove the board owner."); 
        }

        if (requesterMember.Role != BoardMemberRoles.Owner)
        {
            if (requesterMember.Role == BoardMemberRoles.Member)
            {
                if (requesterMember.UserId != targetMember.UserId)
                {
                    throw new UnauthorizedAccessException("Members cannot remove other users."); 
                }
            }

            else if (requesterMember.Role >= targetMember.Role)
            {
                if (requesterMember.UserId != targetMember.UserId)
                {
                    throw new UnauthorizedAccessException("You cannot remove a user with an equal or higher role."); 
                }
            }
        }

        await _unitOfWork.BoardMembers.DeleteAsync(targetMember, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}