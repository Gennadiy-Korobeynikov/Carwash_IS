using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarwashClient.Models
{
    public class EmployeeVM
    {

        public int Id { get; set; }

        [DisplayName("Фамилия")]
        [Required(ErrorMessage = "Необходимо ввести фамилию")]
        [StringLength(255)]
        public string LastName { get; set; } = null!;


        [DisplayName("Имя")]
        [Required(ErrorMessage = "Необходимо ввести имя")]
        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [DisplayName("Отчество")]
        [StringLength(255)]
        public string? MidName { get; set; }

        [DisplayName("Дата устройства")]
        public DateOnly EmploymentDate { get; set; }

        [DisplayName("Табельный номер")]
        [Required(ErrorMessage = "Необходимо ввести табельный номер")]
        public int EmployeeNumber { get; set; }

        [DisplayName("ФИО")]
        public string Name  { get; set; } = string.Empty;
    }
}
