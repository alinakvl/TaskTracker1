namespace TaskTracker.Application.Interfaces.Services;
public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task<string> RegisterAsync(string email, string password, string firstName, string lastName);
    Task<string> GenerateTokenAsync(Guid userId, string email, string role);
}