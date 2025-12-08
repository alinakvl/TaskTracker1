using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Queries.Tasks;

public class GetTasksByListQuery : IRequest<IEnumerable<TaskDto>>
{
    public Guid ListId { get; set; }
}