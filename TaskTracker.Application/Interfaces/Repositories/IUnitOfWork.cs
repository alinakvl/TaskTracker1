using TaskTracker.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Application.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    //specific repo
    IBoardRepository Boards { get; }
    ITaskRepository Tasks { get; }
    IUserRepository Users { get; }
    IBoardMemberRepository BoardMembers { get; }

    //generic repo
    IRepository<TaskList, Guid> TaskLists { get; }
    IRepository<Comment, Guid> Comments { get; }
    IRepository<Attachment, Guid> Attachments { get; }
    IRepository<Label, Guid> Labels { get; }
    IRepository<Activity, Guid> Activities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}