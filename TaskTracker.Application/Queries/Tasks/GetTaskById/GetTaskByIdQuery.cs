using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Queries.Tasks.GetTaskById;

public class GetTaskByIdQuery : IRequest<TaskDetailDto?>
{
    public Guid Id { get; set; }
}
