namespace ToDoList.WebApi.Models;

using ToDoList.Common.Models;

public class ToDoListUserDto
{
    public Guid ToDoListId { get; set; }

    public Guid UserId { get; init; }

    public UserRole Role { get; init; }
}