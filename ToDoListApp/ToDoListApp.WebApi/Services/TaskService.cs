namespace ToDoListApp.WebApi.Services;

using AutoMapper;
using ToDoList.Common.Models;
using ToDoList.WebApi.Models;
using ToDoListApp.Database;
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

    public async Task<TaskUpdateDto> UpdateTask(TaskUpdateDto task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));
        var existingTask = await this.context.TaskItems.FindAsync(task.Id).ConfigureAwait(false);
        if (existingTask == null)
        {
            throw new InvalidOperationException("Task not found.");
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.Priority = Enum.Parse<TaskPriority>(task.Priority);
        existingTask.Status = task.Status;
        existingTask.AssignedUserId = task.AssignedUserId;

        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return task;
    }

    public async Task<Guid> DeleteTask(Guid id)
    {
        var result = await this.context.TaskItems.FindAsync(id).ConfigureAwait(false);
        ArgumentNullException.ThrowIfNull(result);
        this.context.TaskItems.Remove(result);
        await this.context.SaveChangesAsync().ConfigureAwait(false);
        return id;
    }
}