using Microsoft.AspNetCore.Identity;
using ToDoListApp.WebApp.Models;

namespace ToDoListApp.WebApp.Services.ServiceContracts;

public interface IRegistrationService
{
    public Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto);
}