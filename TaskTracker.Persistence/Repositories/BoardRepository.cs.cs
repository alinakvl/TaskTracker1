using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Entities;
using TaskTracker.Persistence.Context;

namespace TaskTracker.Persistence.Repositories;

public class BoardRepository : Repository<Board, Guid>, IBoardRepository
{
    public BoardRepository(TaskTrackerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Board>> GetBoardsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(b => b.Owner)
            .Include(b => b.Members)
            .Where(b => b.OwnerId == userId || b.Members.Any(m => m.UserId == userId))
            .Where(b => !b.IsDeleted && !b.IsArchived)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Board>> GetArchivedBoardsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(b => b.Owner)
            .Where(b => b.IsArchived && !b.IsDeleted)
            .OrderByDescending(b => b.ArchivedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Board?> GetBoardWithDetailsAsync(Guid boardId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(b => b.Owner)
            .Include(b => b.Members).ThenInclude(m => m.User)
            .Include(b => b.TaskLists.Where(tl => !tl.IsDeleted))
                .ThenInclude(tl => tl.Tasks.Where(t => !t.IsDeleted))
                    .ThenInclude(t => t.AssignedUser)
            .Include(b => b.TaskLists)
                .ThenInclude(tl => tl.Tasks)
                    .ThenInclude(t => t.TaskLabels)
                        .ThenInclude(tl => tl.Label)
            .Include(b => b.Labels)
            .FirstOrDefaultAsync(b => b.Id == boardId && !b.IsDeleted, cancellationToken);
    }
}

