using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Queries.BoardMembers.GetBoardMembers;

internal class GetBoardMembersQueryHandler : IRequestHandler<GetBoardMembersQuery, IEnumerable<BoardMemberDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBoardMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BoardMemberDto>> Handle(GetBoardMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _unitOfWork.BoardMembers.GetMembersWithUsersByBoardIdAsync( 
            request.BoardId,
            cancellationToken);

        return _mapper.Map<IEnumerable<BoardMemberDto>>(members);
    }
}
