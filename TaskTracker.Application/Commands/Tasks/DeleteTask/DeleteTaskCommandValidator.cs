using FluentValidation;

namespace TaskTracker.Application.Commands.Tasks.DeleteTask;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Task ID is required.");
    }
}
