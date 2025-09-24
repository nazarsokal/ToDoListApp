using Microsoft.AspNetCore.Identity;
using ToDoListApp.WebApp.Models;

namespace ToDoListApp.WebApp.Services.ServiceContracts;

public interface IAuthService
{
    public Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto);
    public Task<SignInResult> SignInUserAsync(LoginUserDto loginUserDto);
}