using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.TaskLists;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Commands.TaskLists.CreateTaskList;

internal class CreateTaskListCommandHandler : IRequestHandler<CreateTaskListCommand, TaskListDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTaskListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskListDto> Handle(CreateTaskListCommand request, CancellationToken cancellationToken)
    {
        var taskList = new TaskList
        {
            Id = Guid.NewGuid(),
            BoardId = request.BoardId,
            Title = request.Title,
            Position = request.Position,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.TaskLists.AddAsync(taskList, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TaskListDto>(taskList);
    }
}
