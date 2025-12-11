using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Persistence.Context;

namespace TaskTracker.Persistence.Repositories;

public class TaskRepository : Repository<Domain.Entities.Task, Guid>, ITaskRepository
{
    public TaskRepository(TaskTrackerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.Entities.Task>> GetTasksByListIdAsync(Guid listId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(t => t.AssignedUser)
            .Include(t => t.TaskLabels).ThenInclude(tl => tl.Label)
            .Where(t => t.ListId == listId && !t.IsDeleted)
            .OrderBy(t => t.Position)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Domain.Entities.Task>> GetTasksByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(t => t.List).ThenInclude(l => l!.Board)
            .Include(t => t.TaskLabels).ThenInclude(tl => tl!.Label)
            .Where(t => t.AssignedUserId == userId && !t.IsDeleted)
            .OrderBy(t => t.DueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Task?> GetTaskWithDetailsAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(t => t.List).ThenInclude(l => l!.Board)
            .Include(t => t.AssignedUser)
            .Include(t => t.TaskLabels).ThenInclude(tl => tl!.Label)
            .Include(t => t.Comments.Where(c => !c.IsDeleted)).ThenInclude(c => c.User)
            .Include(t => t.Attachments.Where(a => !a.IsDeleted))
            .FirstOrDefaultAsync(t => t.Id == taskId && !t.IsDeleted, cancellationToken);
    }
}
