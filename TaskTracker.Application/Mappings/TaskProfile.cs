using AutoMapper;
using TaskTracker.Domain.Constants;
using TaskTracker.Domain.DTOs.TaskLists;
using TaskTracker.Domain.DTOs.Tasks;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappings;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskList, TaskListDto>()
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks.Where(t => !t.IsDeleted)));

        CreateMap<CreateTaskListDto, TaskList>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());


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
    }
}
