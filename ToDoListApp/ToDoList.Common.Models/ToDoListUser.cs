namespace ToDoList.Common.Models;

public class ToDoListUser
{
    public Guid ToDoListId { get; set; }

    public ToDoList ToDoList { get; set; }

    public Guid UserId { get; init; }

    public UserRole Role { get; init; }
}
