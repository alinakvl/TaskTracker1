using MediatR;

namespace TaskTracker.Application.Commands.Users.DeleteUser;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
