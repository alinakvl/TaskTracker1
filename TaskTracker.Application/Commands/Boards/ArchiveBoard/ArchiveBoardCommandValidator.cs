using FluentValidation;

namespace TaskTracker.Application.Commands.Boards.ArchiveBoard;

public class ArchiveBoardCommandValidator : AbstractValidator<ArchiveBoardCommand>
{
    public ArchiveBoardCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Board ID is required.");
    }
}
