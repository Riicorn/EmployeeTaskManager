using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTaskManager.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Due Date")]
    public DateTime? DueDate { get; set; }

    [Display(Name = "Status")]
    public TaskStatus Status { get; set; } = TaskStatus.Pending;

    [Display(Name = "Priority")]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    [Display(Name = "Employee")]
    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }
}

public enum TaskPriority
{
    Low = 0,
    Medium = 1,
    High = 2
}
