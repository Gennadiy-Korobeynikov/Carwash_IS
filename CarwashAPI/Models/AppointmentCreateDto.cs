namespace CarwashAPI.Models
{
    public class AppointmentCreateDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Cost { get; set; }

        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int SpotId { get; set; }
        public int StatusId { get; set; }

        public List<int> ServiceIds { get; set; } = new();
    }
}
