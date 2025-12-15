using MediatR;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Queries.Activities.GetActivitiesByUser;

public class GetActivitiesByUserQuery : IRequest<IEnumerable<Activity>>
{
    public Guid UserId { get; set; }
    public int Take { get; set; } = 50;
}
