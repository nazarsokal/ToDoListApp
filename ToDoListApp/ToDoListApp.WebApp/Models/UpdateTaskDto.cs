namespace ToDoListApp.WebApp.Models;

public class UpdateTaskDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public string Priority { get; set; }

    public Guid AssignedUserId { get; set; }

    public string Status { get; set; }
}