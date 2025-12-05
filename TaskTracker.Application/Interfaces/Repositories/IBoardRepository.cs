namespace TaskTracker.Application.Interfaces.Repositories;

public interface IBoardRepository : IRepository<Domain.Entities.Board, Guid>
{
    Task<IEnumerable<Domain.Entities.Board>> GetBoardsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Domain.Entities.Board>> GetArchivedBoardsAsync(CancellationToken cancellationToken = default);
    Task<Domain.Entities.Board?> GetBoardWithDetailsAsync(Guid boardId, CancellationToken cancellationToken = default);
}
