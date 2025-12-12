using MediatR;
using TaskTracker.Domain.DTOs.Users;

namespace TaskTracker.Application.Queries.Users.GetUserById;

public class GetUserByIdQuery : IRequest<UserDto?>
{
    public Guid Id { get; set; }
}