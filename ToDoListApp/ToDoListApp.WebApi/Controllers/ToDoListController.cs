using Microsoft.AspNetCore.Mvc;

namespace ToDoListApp.WebApi.Controllers;

public class ToDoListController : Controller
{
    private const string apiBaseUrl = "api/todolist";

    [HttpPost]
    [Route($"{apiBaseUrl}/create")]
    public IActionResult CreateToDoItem()
    {
        return Ok("Create ToDo Item");
    }

    [HttpGet]
    [Route($"{apiBaseUrl}/getall")]
    public IActionResult GetAllToDoItems()
    {
        return Ok("Get All To Do Items");
    }

    [HttpGet]
    [Route($"{apiBaseUrl}/get/{{id}}")]
    public IActionResult GetToDoItemById(int id)
    {
        return Ok(apiBaseUrl + "/" + id);
    }
}