namespace ToDoListApp.WebApp.Services;

using ToDoListApp.WebApp.Models;
using ToDoListApp.WebApp.Services.ServiceContracts;

public class ToDoListService : IToDoListService
{
    private const string BaseUrl = "http://localhost:5186/api/ToDoList";
    private readonly HttpClient httpClient;

    public ToDoListService()
    {
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
}