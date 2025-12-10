using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Commands.Tasks.MoveTask;

internal class MoveTaskCommandHandler : IRequestHandler<MoveTaskCommand, TaskDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MoveTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskDto> Handle(MoveTaskCommand request, CancellationToken cancellationToken)
    {
        
        var task = await _unitOfWork.Tasks.GetByIdAsync(request.TaskId, cancellationToken);

        if (task == null)
        {
            
            throw new KeyNotFoundException($"Task with ID {request.TaskId} not found.");
        }

    
        task.ListId = request.TargetListId;
        task.Position = request.Position;
        task.UpdatedAt = DateTime.UtcNow;

       
        await _unitOfWork.Tasks.UpdateAsync(task, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

       
        return _mapper.Map<TaskDto>(task);
    }
}
