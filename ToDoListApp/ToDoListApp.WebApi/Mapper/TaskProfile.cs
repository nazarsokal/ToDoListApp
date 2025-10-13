namespace ToDoListApp.WebApi.Mapper;

using AutoMapper;
using ToDoList.Common.Models;
using ToDoList.WebApi.Models;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        this.CreateMap<TaskCreateDto, TaskItem>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<TaskPriority>(src.Priority)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => TaskStatus.NotStarted))
            .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedUserId))
            .ForMember(dest => dest.ToDoListId, opt => opt.MapFrom(src => src.ToDoListId))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is generated elsewhere
    }
}