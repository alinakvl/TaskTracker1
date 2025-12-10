using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Queries.Tasks;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Application.Handlers.QueryHandlers;

internal class GetTasksByUserQueryHandler : IRequestHandler<GetTasksByUserQuery, IEnumerable<TaskDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTasksByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetTasksByUserQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.Tasks.GetTasksByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<TaskDto>>(tasks);
    }
}