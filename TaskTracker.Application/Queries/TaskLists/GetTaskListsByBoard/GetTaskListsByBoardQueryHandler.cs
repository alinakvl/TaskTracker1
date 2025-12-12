using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.TaskLists;

namespace TaskTracker.Application.Queries.TaskLists.GetTaskListsByBoard;

internal class GetTaskListsByBoardQueryHandler : IRequestHandler<GetTaskListsByBoardQuery, IEnumerable<TaskListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTaskListsByBoardQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskListDto>> Handle(GetTaskListsByBoardQuery request, CancellationToken cancellationToken)
    {
        var taskLists = await _unitOfWork.TaskLists.FindAsync(
            tl => tl.BoardId == request.BoardId && !tl.IsDeleted,
            cancellationToken);

        return _mapper.Map<IEnumerable<TaskListDto>>(taskLists.OrderBy(tl => tl.Position));
    }
}