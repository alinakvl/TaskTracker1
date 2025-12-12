using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Domain.DTOs.Auth;
using TaskTracker.Application.Commands.Auth.Login;
using TaskTracker.Application.Commands.Auth.Register;
using MediatR;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
   
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[HttpPost("login")]
    //[AllowAnonymous]
    //public async Task<ActionResult<AuthResponseDto>> LoginAsync([FromBody] LoginCommand command)
    //{
    //    try
    //    {
    //        var result = await _mediator.Send(command);
    //        return Ok(result);
    //    }
    //    catch (UnauthorizedAccessException ex)
    //    {
    //        return Unauthorized(new { message = ex.Message });
    //    }
    //}

    //[HttpPost("register")]
    //[AllowAnonymous]
    //public async Task<ActionResult<AuthResponseDto>> RegisterAsync([FromBody] RegisterUserCommand command)
    //{
    //    try
    //    {
    //        var result = await _mediator.Send(command);
    //        return Ok(result);
    //    }
    //    catch (InvalidOperationException ex)
    //    {

    //        return BadRequest(new { message = ex.Message });
    //    }
    //}

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> LoginAsync([FromBody] LoginDto request)
    {
        var command = new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        };

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
 
    public async Task<ActionResult<AuthResponseDto>> RegisterAsync([FromBody] RegisterDto request)
    {
        var command = new RegisterUserCommand
        {
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
