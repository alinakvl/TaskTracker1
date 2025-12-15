using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Commands.TaskLists.CreateTaskList;
using TaskTracker.Application.Commands.TaskLists.DeleteTaskList;
using TaskTracker.Application.Commands.TaskLists.UpdateTaskList;
using TaskTracker.Application.Queries.TaskLists.GetTaskListById;
using TaskTracker.Application.Queries.TaskLists.GetTaskListsByBoard;
using TaskTracker.Domain.DTOs.TaskLists;


namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskListsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskListsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}", Name = "GetTaskListById")]
    public async Task<ActionResult<TaskListDto>> GetByIdAsync(Guid id)
    {
        var query = new GetTaskListByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound(new { message = $"TaskList with ID {id} not found" });

        return Ok(result);
    }

    [HttpGet("board/{boardId}")]
    public async Task<ActionResult<IEnumerable<TaskListDto>>> GetByBoardIdAsync(Guid boardId)
    {
        var query = new GetTaskListsByBoardQuery { BoardId = boardId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskListDto>> CreateAsync([FromBody] CreateTaskListDto dto)
    {
        var command = new CreateTaskListCommand
        {
            BoardId = dto.BoardId,
            Title = dto.Title,
            Position = dto.Position
        };

        var result = await _mediator.Send(command);

        return CreatedAtRoute("GetTaskListById", new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskListDto>> UpdateAsync(Guid id, [FromBody] UpdateTaskListDto dto)
    {
        var command = new UpdateTaskListCommand
        {
            Id = id, 
            Title = dto.Title,
            Position = dto.Position
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
        var command = new DeleteTaskListCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = $"TaskList with ID {id} not found" });

        return NoContent();
    }
}