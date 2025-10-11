namespace ToDoListApp.WebApp.Models;

public class ToDoListDetailDto
{
    public Guid ToDoListId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<string> TasksList { get; set; }

    public string Status { get; set; }

    public ICollection<UserRolesDto> UserRoles { get; set; }
}