using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Commands.Comments.CreateComment;
using TaskTracker.Application.Commands.Comments.DeleteComment;
using TaskTracker.Application.Commands.Comments.UpdateComment;
using TaskTracker.Application.Queries.Comments.GetCommentById;
using TaskTracker.Application.Queries.Comments.GetCommentsByTask;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("{id}", Name = "GetCommentById")]
    public async Task<ActionResult<CommentDto>> GetByIdAsync(Guid id)
    {
        var query = new GetCommentByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"Comment with ID {id} not found" });

        return Ok(result);
    }

    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetByTaskIdAsync(Guid taskId)
    {
        var query = new GetCommentsByTaskQuery { TaskId = taskId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateAsync([FromBody] CreateCommentDto dto)
    {
        var command = new CreateCommentCommand
        {
            UserId = GetCurrentUserId(), 
            TaskId = dto.TaskId,
            Content = dto.Content
        };

        var result = await _mediator.Send(command);

       
        return CreatedAtRoute("GetCommentById", new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CommentDto>> UpdateAsync(Guid id, [FromBody] UpdateCommentDto dto)
    {
        var command = new UpdateCommentCommand
        {
            Id = id,
            UserId = GetCurrentUserId(), 
            Content = dto.Content
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
        catch (UnauthorizedAccessException )
        {
            return Forbid();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var command = new DeleteCommentCommand
        {
            Id = id,
            UserId = GetCurrentUserId() 
        };

        try
        {
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound(new { message = $"Comment with ID {id} not found" });

            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
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

