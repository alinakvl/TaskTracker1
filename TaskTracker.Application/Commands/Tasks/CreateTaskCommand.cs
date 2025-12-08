using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Commands.Tasks;

public class CreateTaskCommand : IRequest<TaskDto>
{
    public Guid ListId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? AssignedUserId { get; set; }
    public int Priority { get; set; } = 2;
    public DateTime? DueDate { get; set; }
}

