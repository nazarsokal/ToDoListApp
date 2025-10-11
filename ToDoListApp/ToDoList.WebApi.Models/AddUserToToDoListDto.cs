namespace ToDoList.WebApi.Models;

public class AddUserToToDoListDto
{
    public Guid Id { get; set; }

    public string email { get; set; }

    public string Role { get; set; }
}