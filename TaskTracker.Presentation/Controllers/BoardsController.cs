using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Commands.Boards;
using TaskTracker.Application.Queries.Boards;
using TaskTracker.Domain.DTOs.Boards;

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

   
    /// Get all boards
   
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetAll()
    {
        var query = new GetAllBoardsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

  
    /// Get board by ID with full details

    [HttpGet("{id}")]
    public async Task<ActionResult<BoardDetailDto>> GetById(Guid id)
    {
        var query = new GetBoardByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"Board with ID {id} not found" });

        return Ok(result);
    }

   
    /// Get boards for current user
  
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetMyBoards()
    {
        var userId = GetCurrentUserId();
        var query = new GetBoardsByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

 
    /// Get boards by specific user ID

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetByUserId(Guid userId)
    {
        var query = new GetBoardsByUserIdQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

 
    /// Get archived boards
   
    [HttpGet("archived")]
    public async Task<ActionResult<IEnumerable<BoardDto>>> GetArchived()
    {
        var query = new GetArchivedBoardsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

   
    /// Create new board
   
    [HttpPost]
    public async Task<ActionResult<BoardDto>> Create([FromBody] CreateBoardDto dto)
    {
        var command = new CreateBoardCommand
        {
            Title = dto.Title,
            Description = dto.Description,
            BackgroundColor = dto.BackgroundColor
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

  
    /// Update board
   
    [HttpPut("{id}")]
    public async Task<ActionResult<BoardDto>> Update(Guid id, [FromBody] UpdateBoardDto dto)
    {
        var command = new UpdateBoardCommand
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            BackgroundColor = dto.BackgroundColor
        };

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


    /// Delete board (soft delete)
  
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteBoardCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = $"Board with ID {id} not found" });

        return NoContent();
    }

   
    /// Archive board
   
    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> Archive(Guid id)
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