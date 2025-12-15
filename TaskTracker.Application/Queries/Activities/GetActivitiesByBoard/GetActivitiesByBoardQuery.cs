using MediatR;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Queries.Activities.GetActivitiesByBoard;

public class GetActivitiesByBoardQuery : IRequest<IEnumerable<Activity>>
{
    public Guid BoardId { get; set; }
    public int Take { get; set; } = 50;
}
