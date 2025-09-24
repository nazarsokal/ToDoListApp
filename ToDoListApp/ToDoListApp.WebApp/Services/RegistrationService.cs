using Microsoft.EntityFrameworkCore;
using ToDoListApp.IdentityDb;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoList.Common.Models;

namespace ToDoListApp.WebApp.Services;

using ToDoListApp.WebApp.Models;
using ToDoListApp.WebApp.Services.ServiceContracts;

public class RegistrationService : IRegistrationService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;

    public RegistrationService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        ArgumentNullException.ThrowIfNull(registerUserDto);
        if (await this.CheckIfUserExists(registerUserDto.Email).ConfigureAwait(false))
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        var user = this.mapper.Map<ApplicationUser>(registerUserDto);

        return await this.userManager.CreateAsync(user, registerUserDto.Password).ConfigureAwait(false);
    }

    private async Task<bool> CheckIfUserExists(string email)
    {
        return await this.userManager.Users.AnyAsync(u => u.Email == email).ConfigureAwait(false);
    }
}