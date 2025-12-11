using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Commands.Users.ChangeUserRole;
using TaskTracker.Application.Commands.Users.DeleteUser;
using TaskTracker.Application.Commands.Users.UpdateUser;
using TaskTracker.Application.Queries.Users.GetAllUsers;
using TaskTracker.Application.Queries.Users.GetUserByEmail;
using TaskTracker.Application.Queries.Users.GetUserById;
using TaskTracker.Domain.DTOs.Users;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
    {
        var query = new GetUserByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"User with ID {id} not found" });

        return Ok(result);
    }

    [HttpGet("email/{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDto>> GetByEmailAsync(string email)
    {
        var query = new GetUserByEmailQuery { Email = email };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"User with email {email} not found" });

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateAsync(Guid id, [FromBody] UpdateUserCommand command)
    {
       
        var currentUserId = GetCurrentUserId();
        if (id != currentUserId && !User.IsInRole("Admin"))
            return Forbid();

        command.Id = id;

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteUserCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = $"User with ID {id} not found" });

        return NoContent();
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetMyProfileAsync()
    {
        var userId = GetCurrentUserId();
        var query = new GetUserByIdQuery { Id = userId };

        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = "User profile not found" });

        return Ok(result);
    }

    [HttpPatch("{userId}/role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeRoleAsync(Guid userId, [FromBody] string role)
    {
       
        var command = new ChangeUserRoleCommand { UserId = userId, Role = role };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = $"User with ID {userId} not found" });

        return NoContent();
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst("userId")?.Value ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }

        return userId;
    }
}