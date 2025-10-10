using Microsoft.AspNetCore.Identity;
using ToDoList.Common.Models;

namespace ToDoListApp.WebApp.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.WebApp.Models;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly UserManager<ApplicationUser> userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
    {
        this.logger = logger;
        this.userManager = userManager;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var user = await this.userManager.GetUserAsync(this.User).ConfigureAwait(false);

        this.ViewBag.UserName = user.FirstName;
        this.ViewBag.LastName = user.LastName;

        return this.View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}