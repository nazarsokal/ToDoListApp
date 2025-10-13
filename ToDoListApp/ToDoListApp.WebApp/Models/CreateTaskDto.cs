namespace ToDoListApp.WebApp.Models;

public class CreateTaskDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public string Priority { get; set; }

    public Guid AssignedUserId { get; set; }

    public Guid ToDoListId { get; set; }
}