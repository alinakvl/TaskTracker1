using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Commands.BoardMembers.AddBoardMember;
using TaskTracker.Application.Commands.BoardMembers.RemoveBoardMember;
using TaskTracker.Application.Commands.BoardMembers.UpdateBoardMemberRole;
using TaskTracker.Application.Queries.BoardMembers.GetBoardMembers;
using TaskTracker.Domain.DTOs.Boards;

namespace TaskTracker.Presentation.Controllers;

[ApiController]
[Route("api/boards/{boardId}/members")]
[Authorize]
public class BoardMembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public BoardMembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoardMemberDto>>> GetBoardMembersAsync(Guid boardId)
    {
        var query = new GetBoardMembersQuery { BoardId = boardId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BoardMemberDto>> AddMemberAsync(Guid boardId, [FromBody] AddBoardMemberDto dto)
    {
        var command = new AddBoardMemberCommand
        {
            BoardId = boardId,
            UserId = dto.UserId,
            Role = dto.Role
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
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveMemberAsync(Guid boardId, Guid userId)
    {
        var command = new RemoveBoardMemberCommand
        {
            BoardId = boardId,
            UserId = userId
        };

        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = "Board member not found" });

        return NoContent();
    }

    [HttpPatch("{userId}/role")]
    public async Task<IActionResult> UpdateRoleAsync(Guid boardId, Guid userId, [FromBody] UpdateBoardMemberRoleDto dto)
    {
        var command = new UpdateBoardMemberRoleCommand
        {
            BoardId = boardId,
            UserId = userId,
            Role = dto.Role
        };

        var result = await _mediator.Send(command);

        if (!result)
            return NotFound(new { message = "Board member not found" });

        return NoContent();
    }
}
