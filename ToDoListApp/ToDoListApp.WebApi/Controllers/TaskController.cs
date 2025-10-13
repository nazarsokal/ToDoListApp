using ToDoList.WebApi.Models;
using ToDoListApp.WebApi.Services.ServiceContracts;

namespace ToDoListApp.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

public class TaskController : Controller
{
    private readonly ITaskService taskService;
    private const string ApiBaseUrl = "api/task";

    public TaskController(ITaskService taskService)
    {
        this.taskService = taskService;
    }

    [HttpPost]
    [Route($"{ApiBaseUrl}/create")]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskCreateDto)
    {
        var result = await this.taskService.CreateTask(taskCreateDto).ConfigureAwait(false);
        if (result != Guid.Empty)
        {
            return this.Ok(result);
        }

        return this.BadRequest();
    }
}