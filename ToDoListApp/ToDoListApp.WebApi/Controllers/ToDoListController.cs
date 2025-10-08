using Microsoft.AspNetCore.Mvc;
using ToDoList.WebApi.Models;
using ToDoList.WebApp.Models.Dto;
using ToDoListApp.WebApi.Services.ServiceContracts;

namespace ToDoListApp.WebApi.Controllers;

public class ToDoListController : Controller
{
    private readonly IToDoListService toDoListService;
    private const string ApiBaseUrl = "api/todolist";

    public ToDoListController(IToDoListService toDoListService)
    {
        this.toDoListService = toDoListService;
    }

    [HttpPost]
    [Route($"{ApiBaseUrl}/create")]
    public async Task<IActionResult> CreateToDoItem(CreateToDoListDto dto)
    {
        Guid? result = await this.toDoListService.CreateToDoListAsync(dto).ConfigureAwait(false);

        return this.Ok($"Create ToDo Item {result}");
    }

    [HttpGet]
    [Route($"{ApiBaseUrl}/getall")]
    public async Task<IActionResult> GetAllToDoItems(Guid userId)
    {
        var result = await this.toDoListService.GetAllToDoListsAsync(userId).ConfigureAwait(false);
        return this.Ok(result);
    }

    [HttpGet]
    [Route($"{ApiBaseUrl}/get/{{id}}")]
    public async Task<IActionResult> GetToDoItemById(Guid id)
    {
        var result = await this.toDoListService.GetToDoListByIdAsync(id).ConfigureAwait(false);
        return this.Ok(result);
    }

    [HttpPut]
    [Route($"{ApiBaseUrl}/update/{{id}}")]
    public async Task<IActionResult> UpdateToDoItem(Guid id, UpdateToDoListDto? dto)
    {
        var result = await this.toDoListService.UpdateToDoListAsync(id, dto).ConfigureAwait(false);

        return this.Ok(result);
    }
}