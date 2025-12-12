using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.TaskLists;

namespace TaskTracker.Application.Commands.TaskLists.UpdateTaskList;

public class UpdateTaskListCommandHandler : IRequestHandler<UpdateTaskListCommand, TaskListDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTaskListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskListDto> Handle(UpdateTaskListCommand request, CancellationToken cancellationToken)
    {
        var taskList = await _unitOfWork.TaskLists.GetByIdAsync(request.Id, cancellationToken);

        if (taskList == null)
            throw new KeyNotFoundException($"TaskList with ID {request.Id} not found");

        taskList.Title = request.Title;
        taskList.Position = request.Position;
        taskList.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.TaskLists.UpdateAsync(taskList, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TaskListDto>(taskList);
    }
}