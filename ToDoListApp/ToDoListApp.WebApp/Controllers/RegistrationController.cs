using ToDoListApp.WebApp.Services.ServiceContracts;

namespace ToDoListApp.WebApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoListApp.WebApp.Models;

public class RegistrationController : Controller
{
    private readonly IRegistrationService registrationService;

    public RegistrationController(IRegistrationService registrationService)
    {
        this.registrationService = registrationService;
    }

    [Route("/Account/Register")]
    public IActionResult Register()
    {
        return this.View();
    }

    [HttpPost]
    [Route("/Account/Register")]
    public async Task<IActionResult> Register(RegisterUserDto user)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(user);
        }

        await this.registrationService.RegisterUserAsync(user).ConfigureAwait(false);
        return this.RedirectToAction("Index", "Home");
    }
}