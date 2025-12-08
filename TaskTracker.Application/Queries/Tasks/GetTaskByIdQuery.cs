using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Queries.Tasks;

public class GetTaskByIdQuery : IRequest<TaskDetailDto?>
{
    public Guid Id { get; set; }
}
