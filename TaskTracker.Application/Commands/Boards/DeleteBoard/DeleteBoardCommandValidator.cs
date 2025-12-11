using FluentValidation;

namespace TaskTracker.Application.Commands.Boards.DeleteBoard;

public class DeleteBoardCommandValidator : AbstractValidator<DeleteBoardCommand>
{
    public DeleteBoardCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Board ID is required.");
    }
}
