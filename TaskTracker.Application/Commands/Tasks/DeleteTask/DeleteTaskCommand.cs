using MediatR;

namespace TaskTracker.Application.Commands.Tasks.DeleteTask;

public class DeleteTaskCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}