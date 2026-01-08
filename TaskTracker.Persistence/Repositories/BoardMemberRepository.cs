using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Entities;
using TaskTracker.Persistence.Context;

namespace TaskTracker.Persistence.Repositories;

public class BoardMemberRepository : Repository<BoardMember, Guid>, IBoardMemberRepository
{
    public BoardMemberRepository(TaskTrackerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<BoardMember>> GetMembersWithUsersByBoardIdAsync(Guid boardId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(bm => bm.User) 
            .Where(bm => bm.BoardId == boardId)
            .AsNoTracking() 
            .ToListAsync(cancellationToken);
    }
}