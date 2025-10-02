using ToDoList.WebApp.Models.Dto;

namespace ToDoListApp.WebApi.Services.ServiceContracts;

public interface IToDoListService
{
    public Task<Guid> CreateToDoListAsync(CreateToDoListDto createToDoListDto);
}