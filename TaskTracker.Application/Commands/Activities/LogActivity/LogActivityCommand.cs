using MediatR;

namespace TaskTracker.Application.Commands.Activities.LogActivity;

public class LogActivityCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public Guid EntityId { get; set; }
    public Guid? BoardId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IPAddress { get; set; }
    public string? UserAgent { get; set; }
}
