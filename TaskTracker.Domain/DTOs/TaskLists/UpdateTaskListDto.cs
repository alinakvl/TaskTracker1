namespace TaskTracker.Domain.DTOs.TaskLists;
public class UpdateTaskListDto
{
    public string Title { get; set; } = string.Empty;
    public int Position { get; set; }
}

