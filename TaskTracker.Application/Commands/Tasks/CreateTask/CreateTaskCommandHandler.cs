using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Commands.Tasks.CreateTask;

internal class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new Domain.Entities.Task
        {
            Id = Guid.NewGuid(),
            ListId = request.ListId,
            Title = request.Title,
            Description = request.Description,
            AssignedUserId = request.AssignedUserId,
            Priority = request.Priority,
            DueDate = request.DueDate,
            Position = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Tasks.AddAsync(task, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdTask = await _unitOfWork.Tasks.GetTaskWithDetailsAsync(task.Id, cancellationToken);
        return _mapper.Map<TaskDto>(createdTask);
    }
}

