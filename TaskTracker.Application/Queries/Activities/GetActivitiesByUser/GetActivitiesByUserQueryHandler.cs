using MediatR;
using TaskTracker.Domain.Entities;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Queries.Activities.GetActivitiesByUser;
internal class GetActivitiesByUserQueryHandler : IRequestHandler<GetActivitiesByUserQuery, IEnumerable<Activity>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetActivitiesByUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Activity>> Handle(GetActivitiesByUserQuery request, CancellationToken cancellationToken)
    {
        var activities = await _unitOfWork.Activities.FindAsync(
            a => a.UserId == request.UserId,
            cancellationToken);

        return activities
            .OrderByDescending(a => a.CreatedAt)
            .Take(request.Take)
            .ToList();
    }
}
