using MediatR;

namespace TaskTracker.Application.Commands.BoardMembers.UpdateBoardMemberRole;
public class UpdateBoardMemberRoleCommand : IRequest<bool>
{
    public Guid BoardId { get; set; }
    public Guid UserId { get; set; }
    public int Role { get; set; }
}