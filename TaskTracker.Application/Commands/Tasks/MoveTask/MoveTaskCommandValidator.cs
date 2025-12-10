using FluentValidation;

namespace TaskTracker.Application.Commands.Tasks.MoveTask;

public class MoveTaskCommandValidator : AbstractValidator<MoveTaskCommand>
{
    public MoveTaskCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("Task ID is required.");

        RuleFor(x => x.TargetListId)
            .NotEmpty().WithMessage("Target List ID is required.");

        RuleFor(x => x.Position)
            .GreaterThanOrEqualTo(0).WithMessage("Position must be 0 or greater.");
    }
}
