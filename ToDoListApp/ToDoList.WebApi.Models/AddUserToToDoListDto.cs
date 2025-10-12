namespace ToDoList.WebApi.Models;

public class AddUserToToDoListDto
{
    public Guid? UserId { get; set; }

    public string email { get; set; }

    public string Role { get; set; }
}