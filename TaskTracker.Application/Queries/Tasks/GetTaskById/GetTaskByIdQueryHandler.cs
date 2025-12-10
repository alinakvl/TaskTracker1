using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Queries.Tasks.GetTaskById;

internal class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDetailDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTaskByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskDetailDto?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetTaskWithDetailsAsync(request.Id, cancellationToken);
        return task == null ? null : _mapper.Map<TaskDetailDto>(task);
    }
}
