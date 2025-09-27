namespace ToDoListApp.WebApp.Models;

public class ToDoListSummarytDto
{
    public Guid ToDoListId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}