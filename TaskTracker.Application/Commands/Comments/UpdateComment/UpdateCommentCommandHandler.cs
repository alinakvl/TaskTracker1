using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Application.Commands.Comments.UpdateComment;
public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(request.Id, cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID {request.Id} not found");

        if (comment.UserId != request.UserId)
            throw new UnauthorizedAccessException("You can only edit your own comments");

        comment.Content = request.Content;
        comment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Comments.UpdateAsync(comment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CommentDto>(comment);
    }
}

