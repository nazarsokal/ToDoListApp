namespace ToDoListApp.WebApp.Models;

public class UserRolesDto
{
    public Guid UserId { get; init; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Role { get; init; }
}
