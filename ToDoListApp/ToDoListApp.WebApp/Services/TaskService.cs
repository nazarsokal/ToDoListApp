namespace ToDoListApp.WebApp.Services;

using ToDoListApp.WebApp.Models;
using ToDoListApp.WebApp.Services.ServiceContracts;

public class TaskService : ITaskService
{
    private const string BaseUrl = "http://localhost:5186/api/task";
    private readonly HttpClient httpClient = new HttpClient();

    public async Task<Guid> CreateTask(CreateTaskDto createTaskDto)
    {
        var response = await this.httpClient.PostAsJsonAsync($"{BaseUrl}/create", createTaskDto)
            .ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Guid>().ConfigureAwait(false);
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        throw new NullReferenceException("Failed to create task from the API.");
    }
}