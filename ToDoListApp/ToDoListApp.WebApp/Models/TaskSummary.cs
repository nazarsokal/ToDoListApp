namespace ToDoListApp.WebApp.Models;

using System.Text.Json.Serialization;

public class TaskSummary
{
    [JsonPropertyName("id")]
    public Guid TaskId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DueDate { get; set; }

    public string? FirstNameAssigned { get; set; }

    public string? LastNameAssigned { get; set; }

    public Guid AssignedUserId { get; set; }

    public string Status { get; set; }
}