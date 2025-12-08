using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Queries.Tasks;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Handlers.QueryHandlers;

public class GetTasksByListQueryHandler : IRequestHandler<GetTasksByListQuery, IEnumerable<TaskDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTasksByListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetTasksByListQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.Tasks.GetTasksByListIdAsync(request.ListId, cancellationToken);
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }
}

