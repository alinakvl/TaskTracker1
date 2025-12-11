using MediatR;
using TaskTracker.Domain.DTOs.Users;

namespace TaskTracker.Application.Queries.Users.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}