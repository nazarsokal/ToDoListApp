namespace ToDoList.WebApi.Models;

public class UpdateToDoListDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public TaskStatus Status { get; set; }
}