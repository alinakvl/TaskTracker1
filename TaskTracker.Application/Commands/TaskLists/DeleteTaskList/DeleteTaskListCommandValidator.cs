using FluentValidation;

namespace TaskTracker.Application.Commands.TaskLists.DeleteTaskList;

public class DeleteTaskListCommandValidator : AbstractValidator<DeleteTaskListCommand>
{
    public DeleteTaskListCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
