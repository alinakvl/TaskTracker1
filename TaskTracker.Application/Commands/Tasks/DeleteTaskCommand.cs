using MediatR;

namespace TaskTracker.Application.Commands.Tasks;

public class DeleteTaskCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}