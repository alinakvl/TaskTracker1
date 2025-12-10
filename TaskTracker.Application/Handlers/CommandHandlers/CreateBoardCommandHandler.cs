using AutoMapper;
using MediatR;
using TaskTracker.Application.Commands.Boards;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Boards;
using TaskTracker.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace TaskTracker.Application.Handlers.CommandHandlers;

internal class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

     public CreateBoardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BoardDto> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        // Get current user ID from claims
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value
                          ?? _httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }

        var board = new Board
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            BackgroundColor = request.BackgroundColor ?? "#0079BF",
            OwnerId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Boards.AddAsync(board, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BoardDto>(board);
    }
}