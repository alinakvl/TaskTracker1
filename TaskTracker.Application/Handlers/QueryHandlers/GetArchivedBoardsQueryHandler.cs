using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Queries.Boards;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Handlers.QueryHandlers;

internal class GetArchivedBoardsQueryHandler : IRequestHandler<GetArchivedBoardsQuery, IEnumerable<BoardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetArchivedBoardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BoardDto>> Handle(GetArchivedBoardsQuery request, CancellationToken cancellationToken)
    {
        var boards = await _unitOfWork.Boards.GetArchivedBoardsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<BoardDto>>(boards);
    }
}
