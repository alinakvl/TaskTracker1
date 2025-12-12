using FluentValidation;

namespace TaskTracker.Application.Commands.TaskLists.CreateTaskList;
public class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
{
    public CreateTaskListCommandValidator()
    {
        RuleFor(x => x.BoardId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
    }
}