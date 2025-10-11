using System.Text.Json.Serialization;

namespace ToDoList.WebApi.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Common.Models;
using TaskStatus = System.Threading.Tasks.TaskStatus;

public class GetToDoListDto
{
    [Key]
    [JsonPropertyName("ToDoListId")]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<TaskItem> Tasks { get; set; }

    public string Status { get; set; }

    public ICollection<ToDoListUserDto> UserRoles { get; set; } = new List<ToDoListUserDto>();
}