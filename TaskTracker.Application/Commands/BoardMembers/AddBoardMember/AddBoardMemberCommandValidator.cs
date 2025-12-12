using FluentValidation;

namespace TaskTracker.Application.Commands.BoardMembers.AddBoardMember;
public class AddBoardMemberCommandValidator : AbstractValidator<AddBoardMemberCommand>
{
    public AddBoardMemberCommandValidator()
    {
        RuleFor(x => x.BoardId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).InclusiveBetween(1, 3); 
    }
}