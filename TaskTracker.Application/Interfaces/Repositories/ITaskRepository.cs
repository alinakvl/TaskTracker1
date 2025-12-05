namespace TaskTracker.Application.Interfaces.Repositories;

public interface ITaskRepository : IRepository<Domain.Entities.Task, Guid>
{
    Task<IEnumerable<Domain.Entities.Task>> GetTasksByListIdAsync(Guid listId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Entities.Task>> GetTasksByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Task?> GetTaskWithDetailsAsync(Guid taskId, CancellationToken cancellationToken = default);
}
