using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Domain.DTOs.Tasks;
using TaskTracker.Application.Queries.Tasks.GetTaskById;
using TaskTracker.Application.Queries.Tasks.GetTasksByList;
using TaskTracker.Application.Queries.Tasks.GetTasksByUser;
using TaskTracker.Application.Commands.Tasks.CreateTask;
using TaskTracker.Application.Commands.Tasks.DeleteTask;
using TaskTracker.Application.Commands.Tasks.UpdateTask;

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

    [HttpGet("{id}", Name = "GetTaskById")]
    public async Task<ActionResult<TaskDetailDto>> GetByIdAsync(Guid id)
    {
        var query = new GetTaskByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"Task with ID {id} not found" });

        return Ok(result);
    }

    [HttpGet("list/{listId}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetByListIdAsync(Guid listId)
    {
        var query = new GetTasksByListQuery { ListId = listId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("my")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetMyTasksAsync()
    {
        var userId = GetCurrentUserId();
        var query = new GetTasksByUserQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetByUserIdAsync(Guid userId)
    {
        var query = new GetTasksByUserQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateAsync([FromBody] CreateTaskDto dto)
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
        //return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        return CreatedAtRoute("GetTaskById", new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> UpdateAsync(Guid id, [FromBody] UpdateTaskDto dto)
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
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