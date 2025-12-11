using FluentValidation;

namespace TaskTracker.Application.Commands.Users.ChangeUserRole;
public class ChangeUserRoleCommandValidator : AbstractValidator<ChangeUserRoleCommand>
{
    public ChangeUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).NotEmpty().Must(r => r == "Admin" || r == "User")
            .WithMessage("Role must be 'Admin' or 'User'");
    }
}