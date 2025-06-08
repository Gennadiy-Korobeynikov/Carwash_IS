using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarwashClient.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string TelNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MidName { get; set; }
        public string? CarInfo { get; set; }
        public string? Preferences { get; set; }
    }
}
