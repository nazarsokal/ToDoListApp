namespace ToDoListApp.WebApp.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Common.Models;
using ToDoListApp.WebApp.Models;
using ToDoListApp.WebApp.Services.ServiceContracts;

public class ToDoListController : Controller
{
    private readonly IToDoListService toDoListService;
    private readonly ITaskService taskService;
    private readonly UserManager<ApplicationUser> userManager;

    public ToDoListController(IToDoListService toDoListService, ITaskService taskService, UserManager<ApplicationUser> userManager)
    {
        this.toDoListService = toDoListService;
        this.taskService = taskService;
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

    [HttpPost]
    [Route("/ToDoList/createtask")]
    public async Task<IActionResult> CreateTask(CreateTaskDto? createTaskDto)
    {
        ArgumentNullException.ThrowIfNull(createTaskDto);
        var result = await this.taskService.CreateTask(createTaskDto).ConfigureAwait(false);
        return this.RedirectToAction("GetTaskById", new { id = createTaskDto.ToDoListId });
    }

    [HttpPost]
    [Route("/ToDoList/updatetask")]
    public async Task<IActionResult> UpdateTask(Guid toDoListId, UpdateTaskDto? updateTaskDto)
    {
        ArgumentNullException.ThrowIfNull(updateTaskDto);
        var result = await this.taskService.UpdateTask(updateTaskDto.Id, updateTaskDto).ConfigureAwait(false);
        return this.RedirectToAction("GetTaskById", new { id = toDoListId });
    }
}