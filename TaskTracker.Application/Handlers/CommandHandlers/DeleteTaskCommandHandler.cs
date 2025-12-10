using MediatR;
using TaskTracker.Application.Commands.Tasks;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Handlers.CommandHandlers;

internal class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken);

        if (task == null)
            return false;

        task.IsDeleted = true;
        task.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Tasks.UpdateAsync(task, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
