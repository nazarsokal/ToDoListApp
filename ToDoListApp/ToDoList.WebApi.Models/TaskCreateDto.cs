namespace ToDoList.WebApi.Models;

public class TaskCreateDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public Guid ToDoListId { get; set; }

    public string Priority { get; set; }

    public Guid AssignedUserId { get; set;  }
}
