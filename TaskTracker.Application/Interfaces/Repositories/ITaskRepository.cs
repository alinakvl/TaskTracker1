namespace TaskTracker.Application.Interfaces.Repositories;

using TaskTracker.Domain.Entities;

public interface ITaskRepository : IRepository<Task, Guid>
{
    Task<IEnumerable<Task>> GetTasksByListIdAsync(Guid listId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Task>> GetTasksByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Task?> GetTaskWithDetailsAsync(Guid taskId, CancellationToken cancellationToken = default);
}
