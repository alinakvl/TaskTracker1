using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Boards;
using TaskTracker.Domain.Entities;



namespace TaskTracker.Application.Commands.Boards.CreateBoard;

internal class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
  

     public CreateBoardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
       
    }

    public async Task<BoardDto> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
       

        var board = new Board
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            BackgroundColor = request.BackgroundColor ?? "#0079BF",
            OwnerId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Boards.AddAsync(board, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BoardDto>(board);
    }
}