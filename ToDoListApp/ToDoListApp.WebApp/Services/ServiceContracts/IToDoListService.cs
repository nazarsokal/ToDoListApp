using ToDoListApp.WebApp.Models;

namespace ToDoListApp.WebApp.Services.ServiceContracts;

public interface IToDoListService
{
    public Task<List<ToDoListSummarytDto>> GetAllTasksAsync(Guid toDoListId);

    public Task<ToDoListDetailDto> GetTaskByIdAsync(Guid taskId);

    public Task<AddUserToToDoListDto> AddUserToToDoListAsync(Guid id, AddUserToToDoListDto? addUserToToDoListDto);
}