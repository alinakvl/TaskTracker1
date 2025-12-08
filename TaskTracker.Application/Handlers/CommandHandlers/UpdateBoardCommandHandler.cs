using AutoMapper;
using MediatR;
using TaskTracker.Application.Commands.Boards;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Application.Handlers.CommandHandlers;

public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, BoardDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBoardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BoardDto> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await _unitOfWork.Boards.GetByIdAsync(request.Id, cancellationToken);

        if (board == null)
            throw new KeyNotFoundException($"Board with ID {request.Id} not found");

        board.Title = request.Title;
        board.Description = request.Description;
        board.BackgroundColor = request.BackgroundColor;
        board.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Boards.UpdateAsync(board, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BoardDto>(board);
    }
}
