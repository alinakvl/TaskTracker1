using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistence.Context;

public class TaskTrackerDbContext : DbContext
{
    public TaskTrackerDbContext(DbContextOptions<TaskTrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<BoardMember> BoardMembers => Set<BoardMember>();
    public DbSet<TaskList> TaskLists => Set<TaskList>();
    public DbSet<Domain.Entities.Task> Tasks => Set<Domain.Entities.Task>();
    public DbSet<Label> Labels => Set<Label>();
    public DbSet<TaskLabel> TaskLabels => Set<TaskLabel>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<Activity> Activities => Set<Activity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Role).IsRequired().HasMaxLength(50).HasDefaultValue("User");
            entity.Property(e => e.AvatarUrl).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasIndex(e => e.Email).IsUnique().HasFilter("IsDeleted = 0");
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.ToTable("Boards");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.BackgroundColor).HasMaxLength(20).HasDefaultValue("#0079BF");
            entity.Property(e => e.IsArchived).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(e => e.Owner)
                .WithMany(u => u.OwnedBoards)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => e.OwnerId).HasFilter("IsDeleted = 0");
        });

        modelBuilder.Entity<BoardMember>(entity =>
        {
            entity.ToTable("BoardMembers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Role).HasDefaultValue(3);
            entity.Property(e => e.JoinedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(e => e.Board)
                .WithMany(b => b.Members)
                .HasForeignKey(e => e.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.BoardMemberships)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.BoardId, e.UserId }).IsUnique();
        });

        modelBuilder.Entity<TaskList>(entity =>
        {
            entity.ToTable("TaskLists");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Position).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(e => e.Board)
                .WithMany(b => b.TaskLists)
                .HasForeignKey(e => e.BoardId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Domain.Entities.Task>(entity =>
        {
            entity.ToTable("Tasks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(300);
            entity.Property(e => e.Position).HasDefaultValue(0);
            entity.Property(e => e.Priority).HasDefaultValue(2);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(e => e.List)
                .WithMany(l => l.Tasks)
                .HasForeignKey(e => e.ListId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(e => e.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => new { e.ListId, e.Position }).HasFilter("IsDeleted = 0");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.ToTable("Labels");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Color).IsRequired().HasMaxLength(20).HasDefaultValue("#61BD4F");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(e => e.Board)
                .WithMany(b => b.Labels)
                .HasForeignKey(e => e.BoardId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TaskLabel>(entity =>
        {
            entity.ToTable("TaskLabels");
            entity.HasKey(e => new { e.TaskId, e.LabelId });

            entity.HasOne(e => e.Task)
                .WithMany(t => t.TaskLabels)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Label)
                .WithMany(l => l.TaskLabels)
                .HasForeignKey(e => e.LabelId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comments");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(e => e.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.ToTable("Attachments");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.BlobUrl).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.ContentType).IsRequired().HasMaxLength(100);
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(e => e.Task)
                .WithMany(t => t.Attachments)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.ToTable("Activities");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ActionType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.EntityType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.IPAddress).HasMaxLength(45);
            entity.Property(e => e.UserAgent).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.HasOne(e => e.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Board)
                .WithMany(b => b.Activities)
                .HasForeignKey(e => e.BoardId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => new { e.EntityType, e.EntityId });
            entity.HasIndex(e => new { e.BoardId, e.CreatedAt });
            entity.HasIndex(e => new { e.UserId, e.CreatedAt });
        });
    }
}



