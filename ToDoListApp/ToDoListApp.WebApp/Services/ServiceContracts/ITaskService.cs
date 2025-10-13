namespace ToDoListApp.WebApp.Services.ServiceContracts;

using ToDoListApp.WebApp.Models;

public interface ITaskService
{
    public Task<Guid> CreateTask(CreateTaskDto createTaskDto);
}