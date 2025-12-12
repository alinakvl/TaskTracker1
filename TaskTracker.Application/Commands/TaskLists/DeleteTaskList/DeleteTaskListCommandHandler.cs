using MediatR;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Commands.TaskLists.DeleteTaskList;

public class DeleteTaskListCommandHandler : IRequestHandler<DeleteTaskListCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskListCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTaskListCommand request, CancellationToken cancellationToken)
    {
        var taskList = await _unitOfWork.TaskLists.GetByIdAsync(request.Id, cancellationToken);

        if (taskList == null)
            return false;

        taskList.IsDeleted = true;
        taskList.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.TaskLists.UpdateAsync(taskList, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}