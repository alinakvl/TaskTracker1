using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Queries.Tasks.GetTasksByUser;

public class GetTasksByUserQuery : IRequest<IEnumerable<TaskDto>>
{
    public Guid UserId { get; set; }
}
