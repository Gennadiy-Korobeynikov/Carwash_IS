using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarwashClient.Models
{
    public class ClientVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Номер телефона обязателен")]
        [StringLength(11, ErrorMessage = "Макс. длина — 11 символов")]
        public string TelNumber { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? LastName { get; set; }

        [StringLength(255)]
        public string? FirstName { get; set; }

        [StringLength(255)]
        public string? MidName { get; set; }

        [StringLength(512)]
        public string? CarInfo { get; set; }

        [StringLength(512)]
        public string? Preferences { get; set; }
    }

}
