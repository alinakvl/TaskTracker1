using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Boards;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Commands.BoardMembers.AddBoardMember;

 internal class AddBoardMemberCommandHandler : IRequestHandler<AddBoardMemberCommand, BoardMemberDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddBoardMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BoardMemberDto> Handle(AddBoardMemberCommand request, CancellationToken cancellationToken)
    {
       
        var board = await _unitOfWork.Boards.GetByIdAsync(request.BoardId, cancellationToken);
        if (board == null)
            throw new KeyNotFoundException($"Board with ID {request.BoardId} not found");

        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {request.UserId} not found");

        var existingMember = await _unitOfWork.BoardMembers.FirstOrDefaultAsync(
            bm => bm.BoardId == request.BoardId && bm.UserId == request.UserId,
            cancellationToken);

        if (existingMember != null)
            throw new InvalidOperationException("User is already a member of this board");

        var boardMember = new BoardMember
        {
            Id = Guid.NewGuid(),
            BoardId = request.BoardId,
            UserId = request.UserId,
            Role = request.Role,
            JoinedAt = DateTime.UtcNow
        };

        await _unitOfWork.BoardMembers.AddAsync(boardMember, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var created = await _unitOfWork.BoardMembers.FirstOrDefaultAsync(
            bm => bm.Id == boardMember.Id,
            cancellationToken);

        return _mapper.Map<BoardMemberDto>(created);
    }
}

