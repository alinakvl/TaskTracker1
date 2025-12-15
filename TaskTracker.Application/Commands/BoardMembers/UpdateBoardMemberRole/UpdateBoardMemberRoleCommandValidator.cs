using FluentValidation;

namespace TaskTracker.Application.Commands.BoardMembers.UpdateBoardMemberRole;

public class UpdateBoardMemberRoleCommandValidator : AbstractValidator<UpdateBoardMemberRoleCommand>
{
    public UpdateBoardMemberRoleCommandValidator()
    {
        RuleFor(x => x.Role).InclusiveBetween(1, 3);
    }
}