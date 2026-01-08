using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Interfaces.Repositories;

public interface IBoardMemberRepository : IRepository<BoardMember, Guid>
{
    Task<IEnumerable<BoardMember>> GetMembersWithUsersByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default);
}