using AutoMapper;
using ToDoList.Common.Models;
using ToDoList.WebApi.Models;
using ToDoList.WebApp.Models.Dto;

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
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => StatusTask.NotStarted))
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => new List<ToDoListUser>
            {
                new()
                {
                    ToDoListId = toDoListId,
                    UserId = src.UserCreatedId,
                    Role = UserRole.Owner,
                },
            }));

        this.CreateMap<ToDoList.Common.Models.ToDoList, GetToDoListDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));

        this.CreateMap<ToDoListUser, ToDoListUserDto>()
            .ForMember(dest => dest.ToDoListId, opt => opt.MapFrom(src => src.ToDoListId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}
