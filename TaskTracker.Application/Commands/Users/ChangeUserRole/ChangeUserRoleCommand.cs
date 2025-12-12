using MediatR;

namespace TaskTracker.Application.Commands.Users.ChangeUserRole;

public class ChangeUserRoleCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public string Role { get; set; } = string.Empty;
}
