// <copyright file="ToDoListService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics;

namespace ToDoListApp.WebApp.Services;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoList.Common.Models;
using ToDoListApp.IdentityDb;
using ToDoListApp.WebApp.Models;
using ToDoListApp.WebApp.Services.ServiceContracts;

public class ToDoListService : IToDoListService
{
    private readonly UserDbContext context;
    private const string BaseUrl = "http://localhost:5186/api/ToDoList";
    private readonly HttpClient httpClient;

    public ToDoListService(UserDbContext context)
    {
        this.context = context;
        this.httpClient = new HttpClient();
    }

    public async Task<List<ToDoListSummarytDto>> GetAllTasksAsync(Guid toDoListId)
    {
        var response = await this.httpClient.GetAsync($"{BaseUrl}/getall?userId={toDoListId}").ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var tasks = await response.Content.ReadFromJsonAsync<List<ToDoListSummarytDto>>().ConfigureAwait(false);
            return await Task.FromResult(tasks ?? new List<ToDoListSummarytDto>()).ConfigureAwait(false);
        }

        // Handle error response
        throw new NullReferenceException("Failed to fetch tasks from the API.");
    }

    public async Task<ToDoListDetailDto> GetTaskByIdAsync(Guid taskId)
    {
        var response = await this.httpClient.GetAsync($"{BaseUrl}/get/{taskId}").ConfigureAwait(false);
        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            var todoList = await response.Content.ReadFromJsonAsync<ToDoListDetailDto>().ConfigureAwait(false);
            foreach (var taskUserRole in todoList!.UserRoles)
            {
                var foundUser = await this.context.Users.FirstOrDefaultAsync(u => u.Id == taskUserRole.UserId).ConfigureAwait(false);
                taskUserRole.FirstName = foundUser?.FirstName;
                taskUserRole.LastName = foundUser?.LastName;
            }

            foreach (var task in todoList!.TasksList)
            {
                var foundUser = await this.context.Users.FirstOrDefaultAsync(u => u.Id == task.AssignedUserId).ConfigureAwait(false);
                task.FirstNameAssigned = foundUser?.FirstName;
                task.LastNameAssigned = foundUser?.LastName;
            }

            return await Task.FromResult(todoList ?? new ToDoListDetailDto()).ConfigureAwait(false);
        }

        throw new NullReferenceException("Failed to fetch task from the API.");
    }

    public async Task<AddUserToToDoListDto> AddUserToToDoListAsync(Guid id, AddUserToToDoListDto? addUserToToDoListDto)
    {
        var foundUser = await this.context.Users.FirstOrDefaultAsync(u => u.Email == addUserToToDoListDto!.email).ConfigureAwait(false);
        if (foundUser == null)
        {
            throw new NullReferenceException("Failed to add user to task from the API.");
        }

        if (addUserToToDoListDto != null)
        {
            addUserToToDoListDto.Id = foundUser!.Id;
            var response = await this.httpClient.PostAsJsonAsync($"{BaseUrl}/adduser/{id}", addUserToToDoListDto)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AddUserToToDoListDto>().ConfigureAwait(false);
                return await Task.FromResult(result ?? new AddUserToToDoListDto()).ConfigureAwait(false);
            }
        }

        throw new NullReferenceException("Failed to fetch user from the API.");
    }
}