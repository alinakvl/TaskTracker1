using MediatR;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Commands.Comments.DeleteComment;
public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(request.Id, cancellationToken);

        if (comment == null) return false;

        if (comment.UserId != request.UserId)
            throw new UnauthorizedAccessException("You can only delete your own comments");

        comment.IsDeleted = true;
        comment.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Comments.UpdateAsync(comment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

