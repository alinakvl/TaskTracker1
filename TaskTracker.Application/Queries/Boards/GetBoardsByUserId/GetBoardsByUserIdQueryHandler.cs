using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.Boards.GetBoardsByUserId;

internal class GetBoardsByUserIdQueryHandler : IRequestHandler<GetBoardsByUserIdQuery, IEnumerable<BoardDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBoardsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BoardDto>> Handle(GetBoardsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var boards = await _unitOfWork.Boards.GetBoardsByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<BoardDto>>(boards);
    }
}
