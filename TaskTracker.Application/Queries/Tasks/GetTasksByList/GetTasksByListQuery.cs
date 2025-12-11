using MediatR;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Queries.Tasks.GetTasksByList;

public class GetTasksByListQuery : IRequest<IEnumerable<TaskDto>>
{
    public Guid ListId { get; set; }
}