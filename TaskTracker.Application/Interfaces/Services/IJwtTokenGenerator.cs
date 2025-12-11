namespace TaskTracker.Application.Interfaces.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string email, string role);
}
