using MediatR;
using TaskTracker.Domain.Entities;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Queries.Activities.GetActivitiesByBoard;
internal class GetActivitiesByBoardQueryHandler : IRequestHandler<GetActivitiesByBoardQuery, IEnumerable<Activity>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetActivitiesByBoardQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Activity>> Handle(GetActivitiesByBoardQuery request, CancellationToken cancellationToken)
    {
        var activities = await _unitOfWork.Activities.FindAsync(
            a => a.BoardId == request.BoardId,
            cancellationToken);

        return activities
            .OrderByDescending(a => a.CreatedAt)
            .Take(request.Take)
            .ToList();
    }
}
