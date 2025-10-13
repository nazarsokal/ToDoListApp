using ToDoList.WebApi.Models;

namespace ToDoListApp.WebApi.Services.ServiceContracts;

public interface ITaskService
{
    public Task<Guid> CreateTask(TaskCreateDto task);
}