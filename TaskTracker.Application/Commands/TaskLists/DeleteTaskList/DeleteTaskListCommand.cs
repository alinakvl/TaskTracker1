using MediatR;

namespace TaskTracker.Application.Commands.TaskLists.DeleteTaskList;

public class DeleteTaskListCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
