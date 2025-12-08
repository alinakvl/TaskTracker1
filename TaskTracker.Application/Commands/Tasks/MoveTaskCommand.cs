using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Commands.Tasks;

public class MoveTaskCommand : IRequest<TaskDto>
{
    public Guid TaskId { get; set; }
    public Guid TargetListId { get; set; }
    public int Position { get; set; }
}