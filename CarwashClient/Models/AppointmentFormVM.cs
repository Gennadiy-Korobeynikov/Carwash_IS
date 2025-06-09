using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarwashClient.Models
{
    public class AppointmentFormVM
    {
        public AppointmentCreateDto Appointment { get; set; } = new();

        //[DisplayName("Дата")]
        //[DataType(DataType.Date)]
        //public DateTime Date { get; set; }


        //[Required(ErrorMessage = "Необходимо ввести время")]
        //[DisplayName("Время")]
        //[DataType(DataType.Time)]
        //public TimeSpan Time { get; set; }



        public List<SelectListItem> Clients { get; set; } = new();
        public List<SelectListItem> Employees { get; set; } = new();
        public List<SelectListItem> Statuses { get; set; } = new();
        public List<SelectListItem> Spots { get; set; } = new();
        public List<SelectListItem> Services { get; set; } = new();
    }
}
