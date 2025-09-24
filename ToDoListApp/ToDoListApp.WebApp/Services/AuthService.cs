using Microsoft.EntityFrameworkCore;
using ToDoListApp.IdentityDb;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using ToDoList.Common.Models;

namespace ToDoListApp.WebApp.Services;

using ToDoListApp.WebApp.Models;
using ToDoListApp.WebApp.Services.ServiceContracts;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IMapper mapper;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.mapper = mapper;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        ArgumentNullException.ThrowIfNull(registerUserDto);
        if (await this.CheckIfUserExists(registerUserDto.Email).ConfigureAwait(false))
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        registerUserDto.UserId = Guid.NewGuid();
        var user = this.mapper.Map<ApplicationUser>(registerUserDto);

        return await this.userManager.CreateAsync(user, registerUserDto.Password).ConfigureAwait(false);
    }

    public async Task<SignInResult> SignInUserAsync(LoginUserDto loginUserDto)
    {
        return await signInManager.PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, true, false).ConfigureAwait(false);
    }

    private async Task<bool> CheckIfUserExists(string email)
    {
        return await this.userManager.Users.AnyAsync(u => u.Email == email).ConfigureAwait(false);
    }
}