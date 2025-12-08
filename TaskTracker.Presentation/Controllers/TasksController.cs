using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Commands.Tasks;
using TaskTracker.Application.Queries.Tasks;
using TaskTracker.Domain.DTOs.Tasks;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

  
    /// Get task by ID with full details
  
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDetailDto>> GetById(Guid id)
    {
        var query = new GetTaskByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"Task with ID {id} not found" });

        return Ok(result);
    }

 
    /// Get tasks by list ID
  
    [HttpGet("list/{listId}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetByListId(Guid listId)
    {
        var query = new GetTasksByListQuery { ListId = listId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

   
    /// Get tasks assigned to current user
  
    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetMyTasks()
    {
        var userId = GetCurrentUserId();
        var query = new GetTasksByUserQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    /// Get tasks by specific user ID
   
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetByUserId(Guid userId)
    {
        var query = new GetTasksByUserQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

   
    /// Create new task
   
    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskDto dto)
    {
        var command = new CreateTaskCommand
        {
            ListId = dto.ListId,
            Title = dto.Title,
            Description = dto.Description,
            AssignedUserId = dto.AssignedUserId,
            Priority = dto.Priority,
            DueDate = dto.DueDate
        };

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

  
    /// Update task
  
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> Update(Guid id, [FromBody] UpdateTaskDto dto)
    {
        var command = new UpdateTaskCommand
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            AssignedUserId = dto.AssignedUserId,
            Priority = dto.Priority,
            DueDate = dto.DueDate
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

  
    /// Delete task (soft delete)
   
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTaskCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = $"Task with ID {id} not found" });

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