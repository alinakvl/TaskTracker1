namespace TaskTracker.Domain.Constants;

public static class TaskPriority
{
    public const int Low = 1;
    public const int Medium = 2;
    public const int High = 3;
    public const int Critical = 4;
    public static string GetPriorityName(int priority) => priority switch
    {
        Low => "Low",
        Medium => "Medium",
        High => "High",
        Critical => "Critical",
        _ => "Unknown"
    };
}
