using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DTOs
{
    public class CreateEmployeeDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MaxLength(100)]
        public string? Department { get; set; }
    }
}
