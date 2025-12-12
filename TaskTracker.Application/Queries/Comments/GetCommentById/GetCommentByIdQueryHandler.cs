using AutoMapper;
using MediatR;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.DTOs.Comments;

namespace TaskTracker.Application.Queries.Comments.GetCommentById;

internal class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCommentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.Comments.GetByIdAsync(request.Id, cancellationToken);
        return comment == null ? null : _mapper.Map<CommentDto>(comment);
    }
}
