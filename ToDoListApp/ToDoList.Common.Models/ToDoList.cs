namespace ToDoList.Common.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ToDoList
{
    [Key]
    [Column("ToDoListId")]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<TaskItem> Tasks { get; set; }

    public TaskStatus Status { get; set; }

    public ICollection<ToDoListUser> UserRoles { get; set; } = new List<ToDoListUser>();

}

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed,
    OnHold
}

public enum UserRole
{
    Owner,
    Contributor,
    Viewer
}