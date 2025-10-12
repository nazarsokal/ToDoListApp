using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Common.Models;
using ToDoListApp.WebApp.Models;
using ToDoListApp.WebApp.Services.ServiceContracts;

namespace ToDoListApp.WebApp.Controllers;

public class ToDoListController : Controller
{
    private readonly IToDoListService toDoListService;
    private readonly UserManager<ApplicationUser> userManager;

    public ToDoListController(IToDoListService toDoListService, UserManager<ApplicationUser> userManager)
    {
        this.toDoListService = toDoListService;
        this.userManager = userManager;
    }

    [HttpGet]
    [Route("/ToDoList")]
    public async Task<IActionResult> Index()
    {
        var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);
        var tasks = await this.toDoListService.GetAllTasksAsync(user.Id).ConfigureAwait(false);
        this.ViewBag.ToDoLists = tasks;
        return this.View();
    }

    [HttpGet]
    [Route("/ToDoList/Get/{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var task = await this.toDoListService.GetTaskByIdAsync(id).ConfigureAwait(false);
        return this.View(task);
    }

    [HttpPost]
    [Route("/ToDoList/adduser")]
    public async Task<IActionResult> AddUserToToDoList(Guid id, string email)
    {
        var dto = new AddUserToToDoListDto()
        {
            email = email,
            Role = "Viewer",
        };
        var result = await this.toDoListService.AddUserToToDoListAsync(id, dto).ConfigureAwait(false);
        return this.RedirectToAction("GetTaskById", new { id = id });
    }
}