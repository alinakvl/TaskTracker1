using FluentValidation;


namespace TaskTracker.Application.Commands.Comments.UpdateComment;
public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Content).NotEmpty().MaximumLength(1000);
    }
}

