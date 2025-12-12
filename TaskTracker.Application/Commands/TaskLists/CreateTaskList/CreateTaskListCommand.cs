using MediatR;
using TaskTracker.Domain.DTOs.TaskLists;

namespace TaskTracker.Application.Commands.TaskLists.CreateTaskList;
public class CreateTaskListCommand : IRequest<TaskListDto>
{
    public Guid BoardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
}

