namespace ToDoListApp.WebApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoListApp.WebApp.Models;

public class RegistrationController : Controller
{
    [Route("/Account/Register")]
    public IActionResult Register()
    {
        return this.View();
    }

    [HttpPost]
    [Route("/Account/Register")]
    public IActionResult Register(RegisterUserDto user)
    {
        return this.RedirectToAction("Index", "Home");
    }
}