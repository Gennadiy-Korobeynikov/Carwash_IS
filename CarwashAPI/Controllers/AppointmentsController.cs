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
    public class AppointmentsController : ControllerBase
    {
        private readonly CarwashDbContext _context;

        public AppointmentsController(CarwashDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            var appointments = await _context.Appointments
                    .Include(a => a.Client)
                    .Include(a => a.Employee)
                    .Include(a => a.Spot)
                    .Include(a => a.Status)
                    .Include(a => a.Services)
                    .Select(a => new AppointmentReadDto
                    {
                        Id = a.Id,
                        Date= a.DateTime,
                        Time = a.DateTime.TimeOfDay,
                        Cost = a.Cost,
                        ClientName = a.Client.Name,
                        EmployeeName = a.Employee.LastName,
                        StatusName = a.Status.Name,
                        SpotNumber = a.Spot.Number,
                        Services = a.Services.Select(s => s.Name).ToList()
                    })
                    .ToListAsync();

            return Ok(appointments);
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentReadDto>> GetAppointment(int id)
        {

            var appointment = await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Employee)
                .Include(a => a.Spot)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentDto = new AppointmentReadDto
            {
                Id = appointment.Id,
                Date = appointment.DateTime.Date,
                Time = appointment.DateTime.TimeOfDay,
                Cost = appointment.Cost,
                ClientName = appointment.Client.Name,
                EmployeeName = appointment.Employee.LastName,
                SpotNumber = appointment.Spot.Number,
                StatusName = appointment.Status.Name,
                Services = appointment.Services.Select(s => s.Name).ToList()
            };


            return appointmentDto;
        }


        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, AppointmentCreateDto dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.DateTime = dto.DateTime;
            appointment.Cost = dto.Cost;
            appointment.ClientId = dto.ClientId;
            appointment.EmployeeId = dto.EmployeeId;
            appointment.StatusId = dto.StatusId;
            appointment.SpotId = dto.SpotId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, e.InnerException?.Message ?? e.Message);
            }

            return NoContent();
        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostAppointment(AppointmentCreateDto dto)
        {

            var appointment = new Appointment
            {
                DateTime = dto.DateTime,
                Cost = dto.Cost,
                ClientId = dto.ClientId,
                EmployeeId = dto.EmployeeId,
                SpotId = dto.SpotId,
                StatusId = dto.StatusId,
            };

            var services = await _context.Services
                .Where(s => dto.ServiceIds.Contains(s.Id))
                .ToListAsync();

            appointment.Services = services;

            _context.Appointments.Add(appointment);
            await TrySave();


            return CreatedAtAction("GetClient", new { id = appointment.Id }, appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await TrySave();

            return NoContent();
        }

        private async Task<int> TrySave()
        {
            try
            {
               return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                // Логируем основную ошибку
                Console.WriteLine("Ошибка при сохранении в БД:");
                Console.WriteLine(ex.Message);

                // Подробности
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Внутреннее исключение:");
                    Console.WriteLine(ex.InnerException.Message);
                }

                throw;
            }
        }




        private bool AppointmentExist(int id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }



    }
}
