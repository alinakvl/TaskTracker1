using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Domain.DTOs.Boards;
using TaskTracker.Application.Commands.Boards.ArchiveBoard;
using TaskTracker.Application.Commands.Boards.CreateBoard;
using TaskTracker.Application.Commands.Boards.DeleteBoard;
using TaskTracker.Application.Commands.Boards.UpdateBoard;
using TaskTracker.Application.Queries.Boards.GetAllBoards;
using TaskTracker.Application.Queries.Boards.GetArchivedBoards;
using TaskTracker.Application.Queries.Boards.GetBoardById;
using TaskTracker.Application.Queries.Boards.GetBoardsByUserId;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BoardsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BoardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

   
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetAllAsync()
    {
        var query = new GetAllBoardsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetBoardById")]
    public async Task<ActionResult<BoardDetailDto>> GetByIdAsync(Guid id)
    {
        var query = new GetBoardByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"Board with ID {id} not found" });

        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetMyBoardsAsync()
    {
        var userId = GetCurrentUserId();
        var query = new GetBoardsByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetByUserIdAsync(Guid userId)
    {
        var query = new GetBoardsByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("archived")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetArchivedAsync()
    {
        var query = new GetArchivedBoardsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BoardDto>> CreateAsync([FromBody] CreateBoardCommand command)
    {
       
        command.UserId = GetCurrentUserId();

        var result = await _mediator.Send(command);
        return CreatedAtRoute("GetBoardById", new { id = result.Id }, result);
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BoardDto>> UpdateAsync(Guid id, [FromBody] UpdateBoardCommand command)
    {

        if (id != command.Id)
            return BadRequest("ID mismatch");

       
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
        var command = new DeleteBoardCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = $"Board with ID {id} not found" });

        return NoContent();
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> ArchiveAsync(Guid id)
    {
        var command = new ArchiveBoardCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = $"Board with ID {id} not found" });

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