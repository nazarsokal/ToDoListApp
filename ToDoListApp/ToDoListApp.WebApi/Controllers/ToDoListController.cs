using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApp.Models.Dto;
using ToDoListApp.WebApi.Services.ServiceContracts;

namespace ToDoListApp.WebApi.Controllers;

public class ToDoListController : Controller
{
    private readonly IToDoListService toDoListService;
    private const string apiBaseUrl = "api/todolist";

    public ToDoListController(IToDoListService toDoListService)
    {
        this.toDoListService = toDoListService;
    }

    [HttpPost]
    [Route($"{apiBaseUrl}/create")]
    public async Task<IActionResult> CreateToDoItem(CreateToDoListDto dto)
    {
        Guid? result = await this.toDoListService.CreateToDoListAsync(dto).ConfigureAwait(false);

        return this.Ok($"Create ToDo Item {result}");
    }

    [HttpGet]
    [Route($"{apiBaseUrl}/getall")]
    public IActionResult GetAllToDoItems()
    {
        return Ok("Get All To Do Items");
    }

    [HttpGet]
    [Route($"{apiBaseUrl}/get/{{id}}")]
    public async Task<IActionResult> GetToDoItemById(Guid id)
    {
        var result = await this.toDoListService.GetToDoListByIdAsync(id).ConfigureAwait(false);
        return this.Ok(result);
    }
}