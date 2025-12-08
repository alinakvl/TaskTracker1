using FluentValidation;
using TaskTracker.Application.Commands.Tasks;

namespace TaskTracker.Application.Validators;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task title is required")
            .MaximumLength(300).WithMessage("Task title cannot exceed 300 characters");

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 4)
            .WithMessage("Priority must be between 1 (Low) and 4 (Critical)");
    }
}