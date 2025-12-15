using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.TaskLists;

namespace TaskTracker.Application.Queries.TaskLists.GetTaskListById;

internal class GetTaskListByIdQueryHandler : IRequestHandler<GetTaskListByIdQuery, TaskListDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTaskListByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskListDto?> Handle(GetTaskListByIdQuery request, CancellationToken cancellationToken)
    {
        var taskList = await _unitOfWork.TaskLists.GetByIdAsync(request.Id, cancellationToken);
        return taskList == null ? null : _mapper.Map<TaskListDto>(taskList);
    }
}
