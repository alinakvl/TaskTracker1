using MediatR;
using TaskTracker.Domain.Entities;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Commands.Activities.LogActivity;

internal class LogActivityCommandHandler : IRequestHandler<LogActivityCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public LogActivityCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(LogActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = new Activity
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ActionType = request.ActionType,
            EntityType = request.EntityType,
            EntityId = request.EntityId,
            BoardId = request.BoardId,
            OldValues = request.OldValues,
            NewValues = request.NewValues,
            IPAddress = request.IPAddress,
            UserAgent = request.UserAgent,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Activities.AddAsync(activity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return activity.Id;
    }
}