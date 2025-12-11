using MediatR;
using TaskTracker.Domain.DTOs.Auth; 

namespace TaskTracker.Application.Commands.Auth.Login;

public class LoginCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}