using MediatR;

namespace TaskTracker.Application.Commands.BoardMembers.RemoveBoardMember;

public class RemoveBoardMemberCommand : IRequest<bool>
{
    public Guid BoardId { get; set; }
    public Guid TargetUserId { get; set; } 
    public Guid CurrentUserId { get; set; }
}
