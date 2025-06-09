using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarwashClient.Models
{
    public class ServiceVM
    {
        public int Id { get; set; }


        [DisplayName("Название")]
        public string Name { get; set; } = null!;

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DisplayName("Длительность")]
        public int Duration { get; set; }
    }
}
