using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Users;

namespace TaskTracker.Application.Queries.Users.SearchUsers;

public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Term))
        {
            return new List<UserDto>();
        }

        var term = request.Term.Trim().ToLower();

        var users = await _unitOfWork.Users.FindAsync(u =>
            u.Email.ToLower().Contains(term) ||
            u.FirstName.ToLower().Contains(term) ||
            u.LastName.ToLower().Contains(term),
            cancellationToken);

        var limitedUsers = users.Take(10);

        return _mapper.Map<IEnumerable<UserDto>>(limitedUsers);
    }
}