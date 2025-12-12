using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Commands.BoardMembers.AddBoardMember;
public class AddBoardMemberCommand : IRequest<BoardMemberDto>
{
    public Guid BoardId { get; set; }
    public Guid UserId { get; set; } 
    public int Role { get; set; } = 3; //member
}
