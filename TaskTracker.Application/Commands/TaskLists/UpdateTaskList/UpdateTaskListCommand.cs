using MediatR;
using TaskTracker.Domain.DTOs.TaskLists;

namespace TaskTracker.Application.Commands.TaskLists.UpdateTaskList;

public class UpdateTaskListCommand : IRequest<TaskListDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
}

