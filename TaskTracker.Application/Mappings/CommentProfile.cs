using AutoMapper;
using TaskTracker.Domain.DTOs.Comments;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappings;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.UserName,
            opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : null))
            .ForMember(dest => dest.UserAvatar,
                opt => opt.MapFrom(src => src.User != null ? src.User.AvatarUrl : null));

        CreateMap<CreateCommentDto, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
