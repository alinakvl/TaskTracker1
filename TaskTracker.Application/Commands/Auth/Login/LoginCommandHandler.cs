using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.DTOs.Auth;

namespace TaskTracker.Application.Commands.Auth.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash ?? string.Empty))
        {
            
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

        return new AuthResponseDto
        {
            Token = token,
            
            ExpiresAt = DateTime.UtcNow.AddMinutes(60) 
        };
    }
}
