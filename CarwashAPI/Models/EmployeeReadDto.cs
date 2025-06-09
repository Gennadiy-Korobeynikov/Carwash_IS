using System.ComponentModel.DataAnnotations.Schema;

namespace CarwashAPI.Models
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }

        public DateOnly EmploymentDate { get; set; }

        public int EmployeeNumber { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
