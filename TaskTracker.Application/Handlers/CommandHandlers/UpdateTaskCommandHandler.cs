using AutoMapper;
using MediatR;
using TaskTracker.Application.Commands.Tasks;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Handlers.CommandHandlers;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(request.Id, cancellationToken);

        if (task == null)
            throw new KeyNotFoundException($"Task with ID {request.Id} not found");

        task.Title = request.Title;
        task.Description = request.Description;
        task.AssignedUserId = request.AssignedUserId;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Tasks.UpdateAsync(task, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedTask = await _unitOfWork.Tasks.GetTaskWithDetailsAsync(task.Id, cancellationToken);
        return _mapper.Map<TaskDto>(updatedTask);
    }
}