using AutoMapper;
using TaskTracker.Domain.DTOs.Boards;
using TaskTracker.Domain.DTOs.Tasks;
using TaskTracker.Domain.DTOs.Users;
using TaskTracker.Domain.DTOs.Comments;
using TaskTracker.Domain.DTOs.Attachments;
using TaskTracker.Domain.DTOs.Labels;
using TaskTracker.Domain.DTOs.TaskLists;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Constants;


namespace TaskTracker.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User Mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Board Mappings
        CreateMap<Board, BoardDto>()
            .ForMember(dest => dest.OwnerName,
                opt => opt.MapFrom(src => $"{src.Owner.FirstName} {src.Owner.LastName}"))
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

        // BoardMember Mappings
        CreateMap<BoardMember, BoardMemberDto>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.UserAvatarUrl,
                opt => opt.MapFrom(src => src.User.AvatarUrl))
            .ForMember(dest => dest.RoleName,
                opt => opt.MapFrom(src => BoardMemberRoles.GetRoleName(src.Role)));

        // TaskList Mappings
        CreateMap<TaskList, TaskListDto>()
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks.Where(t => !t.IsDeleted)));

        CreateMap<CreateTaskListDto, TaskList>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Task Mappings
        CreateMap<Domain.Entities.Task, TaskDto>()
            .ForMember(dest => dest.AssignedUserName,
                opt => opt.MapFrom(src => src.AssignedUser != null
                    ? $"{src.AssignedUser.FirstName} {src.AssignedUser.LastName}"
                    : null))
            .ForMember(dest => dest.AssignedUserAvatar,
                opt => opt.MapFrom(src => src.AssignedUser != null ? src.AssignedUser.AvatarUrl : null))
            .ForMember(dest => dest.PriorityName,
                opt => opt.MapFrom(src => TaskPriority.GetPriorityName(src.Priority)))
            .ForMember(dest => dest.CommentsCount,
                opt => opt.MapFrom(src => src.Comments.Count(c => !c.IsDeleted)))
            .ForMember(dest => dest.AttachmentsCount,
                opt => opt.MapFrom(src => src.Attachments.Count(a => !a.IsDeleted)))
            .ForMember(dest => dest.Labels,
                opt => opt.MapFrom(src => src.TaskLabels.Select(tl => tl.Label)));

        CreateMap<Domain.Entities.Task, TaskDetailDto>()
            .IncludeBase<Domain.Entities.Task, TaskDto>()
            .ForMember(dest => dest.Comments,
                opt => opt.MapFrom(src => src.Comments.Where(c => !c.IsDeleted)))
            .ForMember(dest => dest.Attachments,
                opt => opt.MapFrom(src => src.Attachments.Where(a => !a.IsDeleted)));

        CreateMap<CreateTaskDto, Domain.Entities.Task>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Comment Mappings
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.UserAvatar,
                opt => opt.MapFrom(src => src.User.AvatarUrl));

        CreateMap<CreateCommentDto, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Attachment Mappings
        CreateMap<Attachment, AttachmentDto>();

        // Label Mappings
        CreateMap<Label, LabelDto>();
        CreateMap<CreateLabelDto, Label>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}