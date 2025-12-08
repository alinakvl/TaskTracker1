using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Entities;
using TaskTracker.Infrastructure.Options;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Infrastructure.Services.Auth;

public class JwtTokenService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IUnitOfWork unitOfWork, IOptions<JwtOptions> jwtOptions)
    {
        _unitOfWork = unitOfWork;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        // Verify password (using BCrypt in production)
        if (!VerifyPassword(password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password");

        return await GenerateTokenAsync(user.Id, user.Email, user.Role);
    }

    public async Task<string> RegisterAsync(string email, string password, string firstName, string lastName)
    {
        // Check if email exists
        if (await _unitOfWork.Users.EmailExistsAsync(email))
            throw new InvalidOperationException("Email already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = HashPassword(password),
            FirstName = firstName,
            LastName = lastName,
            Role = Domain.Constants.Roles.User,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return await GenerateTokenAsync(user.Id, user.Email, user.Role);
    }

    public Task<string> GenerateTokenAsync(Guid userId, string email, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtOptions.SecretKey);

        var claims = new List<Claim>
        {
            new Claim("userId", userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(tokenHandler.WriteToken(token));
    }

    private string HashPassword(string password)
    {
        //In production, use BCrypt.Net - Next
         return BCrypt.Net.BCrypt.HashPassword(password);

       
    }

    private bool VerifyPassword(string password, string? hash)
    {
        if (string.IsNullOrEmpty(hash))
            return false;

        // In production, use BCrypt.Net-Next
        return BCrypt.Net.BCrypt.Verify(password, hash);

    }
}
