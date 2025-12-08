using FluentValidation;
using TaskTracker.Application.Commands.Boards;

namespace TaskTracker.Application.Validators;

public class UpdateBoardValidator : AbstractValidator<UpdateBoardCommand>
{
    public UpdateBoardValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Board ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Board title is required")
            .MaximumLength(200).WithMessage("Board title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.BackgroundColor)
            .Matches(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
            .WithMessage("Invalid color format. Use hex format like #0079BF")
            .When(x => !string.IsNullOrEmpty(x.BackgroundColor));
    }
}
