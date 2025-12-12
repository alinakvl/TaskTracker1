using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Application.Queries.Comments.GetCommentsByTask;
public class GetCommentsByTaskQueryHandler : IRequestHandler<GetCommentsByTaskQuery, IEnumerable<CommentDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCommentsByTaskQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByTaskQuery request, CancellationToken cancellationToken)
    {
        var comments = await _unitOfWork.Comments.FindAsync(
            c => c.TaskId == request.TaskId && !c.IsDeleted,
            cancellationToken);

        return _mapper.Map<IEnumerable<CommentDto>>(comments.OrderBy(c => c.CreatedAt));
    }
}
