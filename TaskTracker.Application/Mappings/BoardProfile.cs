using AutoMapper;
using TaskTracker.Domain.Constants;
using TaskTracker.Domain.DTOs.Boards;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappings;

public class BoardProfile : Profile
{
    public BoardProfile()
    {
       
        CreateMap<Board, BoardDto>()
            .ForMember(dest => dest.OwnerName,
            opt => opt.MapFrom(src => src.Owner != null ? $"{src.Owner.FirstName} {src.Owner.LastName}" : null))
            .ForMember(dest => dest.MembersCount,
                opt => opt.MapFrom(src => src.Members.Count))
            .ForMember(dest => dest.TasksCount,
                opt => opt.MapFrom(src => src.TaskLists.SelectMany(tl => tl.Tasks).Count(t => !t.IsDeleted)));

        CreateMap<Board, BoardDetailDto>()
            .IncludeBase<Board, BoardDto>()
            .ForMember(dest => dest.TaskLists, opt => opt.MapFrom(src => src.TaskLists))
            .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.Labels))
            .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members));

        CreateMap<CreateBoardDto, Board>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

       
        CreateMap<BoardMember, BoardMemberDto>()
            .ForMember(dest => dest.UserName,             
                opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : null))
            .ForMember(dest => dest.UserAvatarUrl,       
                opt => opt.MapFrom(src => src.User != null ? src.User.AvatarUrl : null))
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => BoardMemberRoles.GetRoleName(src.Role)));
    }
}
