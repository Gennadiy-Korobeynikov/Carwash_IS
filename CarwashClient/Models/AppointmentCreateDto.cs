using System.ComponentModel;

namespace CarwashClient.Models
{
    public class AppointmentCreateDto
    {
        //  public int Id { get; set; }
        [DisplayName("Дата и время")]
        public DateTime DateTime { get; set; }
        //public decimal Cost { get; set; }

        [DisplayName("Клиент")]
        public int ClientId { get; set; }

        [DisplayName("Мойщик")]
        public int EmployeeId { get; set; }

        [DisplayName("Бокс и место")]
        public int SpotId { get; set; }

        [DisplayName("Статус")]
        public int StatusId { get; set; }

        public List<int>? ServiceIds { get; set; } = new();
    }
}
