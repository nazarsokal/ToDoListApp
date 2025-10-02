using System.Text.Json.Serialization;

namespace ToDoList.WebApp.Models.Dto;

public class CreateToDoListDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    [JsonPropertyName("UserId")]
    public Guid UserCreatedId { get; set; }
}