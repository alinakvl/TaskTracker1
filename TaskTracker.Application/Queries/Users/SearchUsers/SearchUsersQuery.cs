using MediatR;
using TaskTracker.Domain.DTOs.Users; 

namespace TaskTracker.Application.Queries.Users.SearchUsers;

public class SearchUsersQuery : IRequest<IEnumerable<UserDto>>
{
    public string Term { get; set; } = string.Empty;
}