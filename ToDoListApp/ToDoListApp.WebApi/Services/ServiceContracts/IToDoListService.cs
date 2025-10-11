using ToDoList.WebApi.Models;
using ToDoList.WebApp.Models.Dto;

namespace ToDoListApp.WebApi.Services.ServiceContracts;

public interface IToDoListService
{
    public Task<Guid> CreateToDoListAsync(CreateToDoListDto createToDoListDto);

    public Task<GetToDoListDto> GetToDoListByIdAsync(Guid id);

    public Task<List<GetToDoListDto>> GetAllToDoListsAsync(Guid userId);

    public Task<UpdateToDoListDto> UpdateToDoListAsync(Guid id, UpdateToDoListDto? updateToDoListDto);

    public Task<AddUserToToDoListDto?> AddUserToToDoListAsync(Guid id, AddUserToToDoListDto? addUserToToDoListDto);
}