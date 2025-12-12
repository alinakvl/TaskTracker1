using FluentValidation;

namespace TaskTracker.Application.Commands.Comments.DeleteComment;
public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
