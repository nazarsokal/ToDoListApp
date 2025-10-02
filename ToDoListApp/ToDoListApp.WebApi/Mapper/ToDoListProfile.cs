using AutoMapper;
using ToDoList.Common.Models;
using ToDoList.WebApp.Models.Dto;
using TaskStatus = ToDoList.Common.Models.TaskStatus;

namespace ToDoListApp.WebApi.Mapper;

public class ToDoListProfile : Profile
{
    public ToDoListProfile()
    {
        var toDoListId = Guid.NewGuid();
        this.CreateMap<CreateToDoListDto, ToDoList.Common.Models.ToDoList>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => toDoListId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Tasks, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => TaskStatus.NotStarted))
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => new List<ToDoListUser>
            {
                new()
                {
                    ToDoListId = toDoListId,
                    UserId = src.UserCreatedId,
                    Role = UserRole.Owner,
                },
            }));
    }
}
