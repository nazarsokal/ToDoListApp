namespace ToDoList.WebApi.Models;

public class TaskSummaryReturnDto
{
    public Guid TaskId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DueDate { get; set; }

    public string UserNameAssigned { get; set; }

    public string Status { get; set; }
}