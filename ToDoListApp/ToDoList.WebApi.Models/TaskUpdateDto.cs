namespace ToDoList.WebApi.Models;

using ToDoList.Common.Models;

public class TaskUpdateDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public StatusTask Status { get; set; }

    public string Priority { get; set; }

    public Guid AssignedUserId { get; set; }
}