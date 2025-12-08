using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Queries.Boards;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Handlers.QueryHandlers;

public class GetBoardByIdQueryHandler : IRequestHandler<GetBoardByIdQuery, BoardDetailDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBoardByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BoardDetailDto?> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
    {
        var board = await _unitOfWork.Boards.GetBoardWithDetailsAsync(request.Id, cancellationToken);
        return board == null ? null : _mapper.Map<BoardDetailDto>(board);
    }
}
