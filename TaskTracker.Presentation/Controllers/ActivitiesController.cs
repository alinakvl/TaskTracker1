using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Queries.Activities.GetActivitiesByBoard;
using TaskTracker.Application.Queries.Activities.GetActivitiesByUser;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("board/{boardId}")]
    public async Task<ActionResult<IEnumerable<Activity>>> GetByBoardAsync(Guid boardId, [FromQuery] int take = 50)
    {
        var query = new GetActivitiesByBoardQuery
        {
            BoardId = boardId,
            Take = take
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Activity>>> GetByUserAsync(Guid userId, [FromQuery] int take = 50)
    {
        var query = new GetActivitiesByUserQuery
        {
            UserId = userId,
            Take = take
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

