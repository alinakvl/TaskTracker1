namespace TaskTracker.Domain.DTOs.TaskLists;
using TaskTracker.Domain.DTOs.Tasks;

public class TaskListDto
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
    public List<TaskDto> Tasks { get; set; } = new();
}
