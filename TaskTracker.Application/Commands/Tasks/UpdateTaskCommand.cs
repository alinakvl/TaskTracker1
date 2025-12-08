using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Commands.Tasks;

public class UpdateTaskCommand : IRequest<TaskDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? AssignedUserId { get; set; }
    public int Priority { get; set; }
    public DateTime? DueDate { get; set; }
}

