using Microsoft.AspNetCore.Mvc;

namespace ToDoListApp.WebApp.Controllers;

public class ToDoListController : Controller
{
    [Route("/ToDoList")]
    public IActionResult Index()
    {
        return this.View();
    }
}