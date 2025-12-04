using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskTracker.Domain.Constants;
//
public static class TaskStatus
{
    public const string Todo = "Todo";
    public const string InProgress = "InProgress";
    public const string InReview = "InReview";
    public const string Done = "Done";
    public const string Blocked = "Blocked";
}