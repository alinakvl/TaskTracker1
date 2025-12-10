using TaskTracker.Domain.DTOs.TaskLists;
using TaskTracker.Domain.DTOs.Labels;

namespace TaskTracker.Domain.DTOs.Boards;

public class BoardDetailDto : BoardDto
{
    public List<TaskListDto> TaskLists { get; set; } = new();
    public List<LabelDto> Labels { get; set; } = new();
    public List<BoardMemberDto> Members { get; set; } = new();
}
