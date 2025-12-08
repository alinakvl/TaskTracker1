using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Queries.Tasks;

public class GetTasksByUserQuery : IRequest<IEnumerable<TaskDto>>
{
    public Guid UserId { get; set; }
}
