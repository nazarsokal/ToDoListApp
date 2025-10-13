using System.Text.Json.Serialization;

namespace ToDoListApp.WebApp.Models;

public class ToDoListDetailDto
{
    public Guid ToDoListId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    [JsonPropertyName("tasks")]
    public List<TaskSummary> TasksList { get; set; }

    public string Status { get; set; }

    public ICollection<UserRolesDto> UserRoles { get; set; }
}