using ToDoListApp.WebApp.Services.ServiceContracts;

namespace ToDoListApp.WebApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using ToDoListApp.WebApp.Models;

public class RegistrationController : Controller
{
    private readonly IAuthService _authService;

    public RegistrationController(IAuthService authService)
    {
        this._authService = authService;
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

        var result = await this._authService.RegisterUserAsync(user).ConfigureAwait(false);
        if (result.Succeeded)
        {
            return this.RedirectToAction("Index", "Home");
        }

        this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return this.View(user);
    }

    [Route("/Account/Login")]
    public IActionResult Login()
    {
        return this.View();
    }

    [HttpPost]
    [Route("/Account/Login")]
    public async Task<IActionResult> Login(LoginUserDto user)
    {
        if (!ModelState.IsValid)
        {
            return this.View(user);
        }

        var result = await this._authService.SignInUserAsync(user).ConfigureAwait(false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "Invalid login attempt.");
        return View(user);
    }
}