using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs
{
    public class UpdateEmployeeDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string? Department { get; set; }
    }
}
