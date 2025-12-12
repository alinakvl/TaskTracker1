using MediatR;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.BoardMembers.GetBoardMembers;
public class GetBoardMembersQuery : IRequest<IEnumerable<BoardMemberDto>>
{
    public Guid BoardId { get; set; }
}

