using ToDoListApp.WebApp.Models;

namespace ToDoListApp.WebApp.Services.ServiceContracts;

public interface IToDoListService
{
    public Task<List<ToDoListSummarytDto>> GetAllTasksAsync(Guid toDoListId);
}