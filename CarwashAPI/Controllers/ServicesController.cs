using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarwashAPI.Models;

namespace CarwashAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly CarwashDbContext _context;

        public ServicesController(CarwashDbContext context)
        {
            _context = context;
        }


        // GET: api/StatusesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetStatuses()
        {
            return await _context.Services.ToListAsync();
        }
    }
}
