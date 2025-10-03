namespace ToDoList.Common.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaskItem
{
    [Key]
    [Column("TaskId")]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DueDate { get; set; }

    public Guid AssignedUserId { get; set; }

    public TaskStatus Status { get; set; }
}