using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Queries.Boards;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Handlers.QueryHandlers;

public class GetAllBoardsQueryHandler : IRequestHandler<GetAllBoardsQuery, IEnumerable<BoardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllBoardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BoardDto>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
    {
        var boards = await _unitOfWork.Boards.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<BoardDto>>(boards);
    }
}