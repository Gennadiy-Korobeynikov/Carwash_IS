using CarwashAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarwashAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {

        private readonly CarwashDbContext _context;

        public StatusesController(CarwashDbContext context)
        {
            _context = context;
        }




        // GET: api/StatusesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>>GetStatuses()
        {
            return await _context.Statuses.ToListAsync() ;
        }

        //// GET api/<StatusesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<StatusesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<StatusesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<StatusesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
