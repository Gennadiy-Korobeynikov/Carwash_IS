using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarwashClient.Models
{
    public class AppointmentVM
    {

        public int Id { get; set; }


        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required(ErrorMessage = "Необходимо ввести время")]
        [DisplayName("Время")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }

        [DisplayName("Клиент")]
        public string ClientName { get; set; } = string.Empty;

        [DisplayName("Мойщик")]
        public string EmployeeName { get; set; } = string.Empty;

        [DisplayName("Бокс и место")]
        public string SpotNumber { get; set; } = string.Empty;

        [DisplayName("Статус")]
        public string StatusName { get; set; } = string.Empty;

        public string[] Services { get; set; } = [];
    }
}
