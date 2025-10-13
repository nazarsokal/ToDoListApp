using AutoMapper;
using ToDoList.Common.Models;
using ToDoListApp.Database;

namespace ToDoListApp.WebApi.Services;

using ToDoList.WebApi.Models;
using ToDoListApp.WebApi.Services.ServiceContracts;

public class TaskService : ITaskService
{
    private readonly ToDoListDbContext context;
    private readonly IMapper mapper;

    public TaskService(ToDoListDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Guid> CreateTask(TaskCreateDto task)
    {
        TaskItem mappedTask = this.mapper.Map<TaskItem>(task);
        mappedTask.Id = Guid.NewGuid();
        this.context.TaskItems.Add(mappedTask);
        var result = await this.context.SaveChangesAsync().ConfigureAwait(false);
        return mappedTask.Id;
    }
}