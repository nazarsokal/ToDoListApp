namespace ToDoListApp.WebApi.Services.ServiceContracts;

using ToDoList.Common.Models;
using ToDoList.WebApi.Models;

public interface ITaskService
{
    public Task<Guid> CreateTask(TaskCreateDto task);

    public Task<TaskUpdateDto> UpdateTask(TaskUpdateDto task);

    public Task<Guid> DeleteTask(Guid id);
}