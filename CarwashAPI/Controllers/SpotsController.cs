using CarwashAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarwashAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : Controller
    {


        private readonly CarwashDbContext _context;

        public SpotsController(CarwashDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpotDto>>> GetSpots()
        {
            return await _context.Spots
                .Select(s => new SpotDto
                {
                    Id = s.Id,
                    Name = $"Бокс: {s.BoxId}, Место: {s.Number}"
                })
                .ToListAsync();
        }

        public class SpotDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
}
