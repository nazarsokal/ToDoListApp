using AutoMapper;
using ToDoList.Common.Models;
using ToDoListApp.WebApp.Models;

namespace ToDoListApp.WebApp.Mapper;

public class MapUser : Profile
{
    public MapUser()
    {
        CreateMap<RegisterUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // handled by UserManager
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // let Identity assign
    }
}