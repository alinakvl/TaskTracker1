using AutoMapper;
using TaskTracker.Domain.DTOs.Labels;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappings;

public class LabelProfile : Profile
{
    public LabelProfile()
    {
        CreateMap<Label, LabelDto>();
        CreateMap<CreateLabelDto, Label>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
    }
}
