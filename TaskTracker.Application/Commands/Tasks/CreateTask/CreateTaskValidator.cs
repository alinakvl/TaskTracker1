using FluentValidation;


namespace TaskTracker.Application.Commands.Tasks.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.ListId)
            .NotEmpty().WithMessage("List ID is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Task title is required")
            .MaximumLength(300).WithMessage("Task title cannot exceed 300 characters");

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 4)
            .WithMessage("Priority must be between 1 (Low) and 4 (Critical)");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Due date must be in the future")
            .When(x => x.DueDate.HasValue);
    }
}
