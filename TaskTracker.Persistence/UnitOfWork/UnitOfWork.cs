using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Entities;
using TaskTracker.Persistence.Context;
using TaskTracker.Persistence.Repositories;

namespace TaskTracker.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly TaskTrackerDbContext _context;

    private IBoardRepository? _boards;
    private ITaskRepository? _tasks;
    private IUserRepository? _users;
    private IRepository<TaskList, Guid>? _taskLists;
    private IRepository<Comment, Guid>? _comments;
    private IRepository<Attachment, Guid>? _attachments;
    private IRepository<Label, Guid>? _labels;
    private IRepository<BoardMember, Guid>? _boardMembers;
    private IRepository<Activity, Guid>? _activities;

    public UnitOfWork(TaskTrackerDbContext context)
    {
        _context = context;
    }

    public IBoardRepository Boards =>
        _boards ??= new BoardRepository(_context);

    public ITaskRepository Tasks =>
        _tasks ??= new TaskRepository(_context);

    public IUserRepository Users =>
        _users ??= new UserRepository(_context);

    public IRepository<TaskList, Guid> TaskLists =>
        _taskLists ??= new Repository<TaskList, Guid>(_context);

    public IRepository<Comment, Guid> Comments =>
        _comments ??= new Repository<Comment, Guid>(_context);

    public IRepository<Attachment, Guid> Attachments =>
        _attachments ??= new Repository<Attachment, Guid>(_context);

    public IRepository<Label, Guid> Labels =>
        _labels ??= new Repository<Label, Guid>(_context);

    public IRepository<BoardMember, Guid> BoardMembers =>
        _boardMembers ??= new Repository<BoardMember, Guid>(_context);

    public IRepository<Activity, Guid> Activities =>
        _activities ??= new Repository<Activity, Guid>(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Automatic timestamp updates
        var entries = _context.ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                       (e.State == Microsoft.EntityFrameworkCore.EntityState.Added ||
                        e.State == Microsoft.EntityFrameworkCore.EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}