using MediatR;
using TaskTracker.Application.Interfaces.Repositories;

namespace TaskTracker.Application.Commands.Users.ChangeUserRole;

internal class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeUserRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
            return false;

        user.Role = request.Role;
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Users.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}