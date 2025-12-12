using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Comments;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Commands.Comments.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            TaskId = request.TaskId,
            UserId = request.UserId,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Comments.AddAsync(comment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CommentDto>(comment);
    }
}