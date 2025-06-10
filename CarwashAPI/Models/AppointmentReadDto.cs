namespace CarwashAPI.Models
{
    public class AppointmentReadDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }
        public decimal Cost { get; set; }

        public string ClientName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string SpotNumber { get; set; } = string.Empty ;
        public string StatusName { get; set; } = string.Empty;

        public List<string> Services { get; set; } = new();
    }
}
