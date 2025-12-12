using MediatR;
using TaskTracker.Domain.DTOs.TaskLists;

namespace TaskTracker.Application.Queries.TaskLists.GetTaskListById;

public class GetTaskListByIdQuery : IRequest<TaskListDto?>
{
    public Guid Id { get; set; }
}