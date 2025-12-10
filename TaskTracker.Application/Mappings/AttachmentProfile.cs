using AutoMapper;
using TaskTracker.Domain.DTOs.Attachments;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappings;

public class AttachmentProfile : Profile
{
    public AttachmentProfile()
    {
        CreateMap<Attachment, AttachmentDto>();
    }
}
