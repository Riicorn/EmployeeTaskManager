using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskManager.Models;

public class Employee
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Department { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Date of Joining")]
    public DateTime DateOfJoining { get; set; } = DateTime.Today;

    public ICollection<TaskItem>? Tasks { get; set; }
}
