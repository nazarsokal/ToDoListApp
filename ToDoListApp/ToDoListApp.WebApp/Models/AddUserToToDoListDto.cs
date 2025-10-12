using System.Text.Json.Serialization;

namespace ToDoListApp.WebApp.Models;

public class AddUserToToDoListDto
{
    [JsonPropertyName("UserId")]
    public Guid Id { get; set; }

    public string email { get; set; }

    public string Role { get; set; }
}