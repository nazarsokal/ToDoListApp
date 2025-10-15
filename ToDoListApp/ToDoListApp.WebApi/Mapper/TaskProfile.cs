namespace ToDoListApp.WebApi.Mapper;

using AutoMapper;
using ToDoList.Common.Models;
using ToDoList.WebApi.Models;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        this.CreateMap<TaskCreateDto, TaskItem>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriority>(src.Priority)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => StatusTask.NotStarted))
            .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedUserId))
            .ForMember(dest => dest.ToDoListId, opt => opt.MapFrom(src => src.ToDoListId))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is generated elsewhere

        this.CreateMap<TaskUpdateDto, TaskItem>()
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate)) // handle typo in DTO
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriority>(src.Priority)))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedUserId))
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
            .ForMember(dest => dest.ToDoListId, opt => opt.Ignore());
    }
}