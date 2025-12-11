using MediatR;
using System.Text.Json.Serialization;
using TaskTracker.Domain.DTOs.Users;

namespace TaskTracker.Application.Commands.Users.UpdateUser;

public class UpdateUserCommand : IRequest<UserDto>
{
    [JsonIgnore] 
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
}

