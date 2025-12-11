using MediatR;
using TaskTracker.Domain.DTOs.Users;

namespace TaskTracker.Application.Queries.Users.GetUserByEmail;

public class GetUserByEmailQuery : IRequest<UserDto?>
{
    public string Email { get; set; } = string.Empty;
}