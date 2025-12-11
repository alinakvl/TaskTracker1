using MediatR;
using TaskTracker.Domain.DTOs.Auth;

namespace TaskTracker.Application.Commands.Auth.Register;

public class RegisterUserCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}