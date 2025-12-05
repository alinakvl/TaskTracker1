namespace TaskTracker.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    //specific repo
    IBoardRepository Boards { get; }
    ITaskRepository Tasks { get; }
    IUserRepository Users { get; }

    //generic repo
    IRepository<Domain.Entities.TaskList, Guid> TaskLists { get; }
    IRepository<Domain.Entities.Comment, Guid> Comments { get; }
    IRepository<Domain.Entities.Attachment, Guid> Attachments { get; }
    IRepository<Domain.Entities.Label, Guid> Labels { get; }
    IRepository<Domain.Entities.BoardMember, Guid> BoardMembers { get; }
    IRepository<Domain.Entities.Activity, Guid> Activities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}