using MediatR;
using TaskTracker.Domain.DTOs.TaskLists;

namespace TaskTracker.Application.Queries.TaskLists.GetTaskListsByBoard;

public class GetTaskListsByBoardQuery : IRequest<IEnumerable<TaskListDto>>
{
    public Guid BoardId { get; set; }
}
