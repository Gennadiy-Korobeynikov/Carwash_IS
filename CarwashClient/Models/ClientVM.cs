using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarwashClient.Models
{
    public class ClientVM
    {
        public int Id { get; set; }

        [DisplayName("Номер телефона")]
        [Required(ErrorMessage = "Необходимо ввести номер телефона")]
        [StringLength(11, ErrorMessage = "Макс. длина — 11 символов")]
        [Phone(ErrorMessage = "Некорректный формат номера телефона")]
        public string TelNumber { get; set; }

        [DisplayName("Обращение")]
        [StringLength(255)]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        [StringLength(255)]
        public string? LastName { get; set; }

        [DisplayName("Имя")]
        [StringLength(255)]
        public string? FirstName { get; set; }

        [DisplayName("Отчество")]
        [StringLength(255)]
        public string? MidName { get; set; }

        [StringLength(512)]
        [DisplayName("Автомобиль")]
        public string? CarInfo { get; set; }

        [StringLength(512)]
        [DisplayName("Предпочтения")]
        public string? Preferences { get; set; }
    }

}
