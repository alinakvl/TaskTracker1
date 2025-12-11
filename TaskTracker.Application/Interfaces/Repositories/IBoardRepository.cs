using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Interfaces.Repositories;

public interface IBoardRepository : IRepository<Board, Guid>
{
    Task<IEnumerable<Board>> GetBoardsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Board>> GetArchivedBoardsAsync(CancellationToken cancellationToken = default);
    Task<Board?> GetBoardWithDetailsAsync(Guid boardId, CancellationToken cancellationToken = default);
}
