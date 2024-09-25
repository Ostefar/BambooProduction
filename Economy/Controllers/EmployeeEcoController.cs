using Economy.Data;
using Economy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.Interfaces;
using System;
using System.Linq;

namespace Economy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEcoController : ControllerBase
    {
        private readonly EconomyDbContext _context;
        private HttpClient _employeeApiClient;
        private readonly IConverter<EmployeeEco, EmployeeEcoDto> taskConverter;

        public EmployeeEcoController(EconomyDbContext context, IHttpClientFactory httpClientFactory, IConverter<EmployeeEco, EmployeeEcoDto> converter)
        {
            _context = context;
            _employeeApiClient = httpClientFactory.CreateClient("EmployeeApi");
            taskConverter = converter;
        }

        // GET: api/EmployeeEco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeEcoDto>>> GetEmployeeEcos()
        {
            // Call the GetEmployeeNames API endpoint
            HttpResponseMessage response = await _employeeApiClient.GetAsync("Employee/names");
            var employeeNames = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();

            if (employeeNames is not null)
            {
                var employeeNameDictionary = employeeNames.ToDictionary(e => e.Id, e => e.FullName);

                var employeeEcos = await _context.EmployeeEcos
                    .Select(e => new EmployeeEcoDto
                    {
                        Id = e.Id,
                        FullName = employeeNameDictionary.ContainsKey(e.EmployeeId) ? employeeNameDictionary[e.EmployeeId] : "Unknown Employee",
                        HourlyWage = e.HourlyWage,
                        SickDaysTotal = e.SickDaysTotal,
                        VacationDaysTotal = e.VacationDaysTotal
                    }).ToListAsync();

                return Ok(employeeEcos);
            }
            else
            {
                return BadRequest("No employees was found.");
            }
        }

        // GET: api/EmployeeEco/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEcoDto>> GetEmployeeEco(Guid id)
        {
            var employeeEco = await _context.EmployeeEcos
                .Where(e => e.Id == id)
                .Select(e => new EmployeeEcoDto
                {
                    Id = e.Id,
                    EmployeeId = e.EmployeeId,
                    HourlyWage = e.HourlyWage,
                    SickDaysTotal = e.SickDaysTotal,
                    VacationDaysTotal = e.VacationDaysTotal,
                    CreatedDate = e.CreatedDate,
                    LastUpdated = e.LastUpdated,
                    LastUpdatedBy = e.LastUpdatedBy

                })
                .FirstOrDefaultAsync();

            if (employeeEco == null)
            {
                return NotFound();
            }

            return Ok(employeeEco);
        }

        // GET: api/EmployeeEco/{id}
        [HttpGet("{id}/GetHourlyWageByEmployeeId")]
        public async Task<ActionResult<EmployeeEcoDto>> GetHourlyWageByEmployeeId(Guid id)
        {
            var employeeEco = await _context.EmployeeEcos
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeEcoDto
                {
                    Id = e.Id,
                    HourlyWage = e.HourlyWage,
                })
                .FirstOrDefaultAsync();

            if (employeeEco == null)
            {
                return NotFound();
            }

            return Ok(employeeEco);
        }

        // POST: api/EmployeeEco
        [HttpPost]
        public async Task<ActionResult<EmployeeEco>> PostEmployeeEco(EmployeeEcoDto employeeEco)
        {
            // Convert dto to model
            var convertedEmployeeEco = taskConverter.Convert(employeeEco);

            _context.EmployeeEcos.Add(convertedEmployeeEco);
            await _context.SaveChangesAsync();

            return Ok(convertedEmployeeEco);
        }

        // PUT: api/EmployeeEco/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeEco(Guid id, EmployeeEco employeeEco)
        {
            var existingEmployeeEco = await _context.EmployeeEcos.FirstOrDefaultAsync(e => e.Id == id);

            if (id != employeeEco.Id)
            {
                return BadRequest();
            }

            // Update employeeEco details
            existingEmployeeEco.HourlyWage = employeeEco.HourlyWage;
            existingEmployeeEco.LastUpdated = DateTime.UtcNow;
            existingEmployeeEco.LastUpdatedBy = employeeEco.LastUpdatedBy;


            // Recalculate SickDaysTotal and VacationDaysTotal
            //employeeEco.SickDaysTotal = CalculateSickDaysTotal(employeeEco.SickLeaves);
            //employeeEco.VacationDaysTotal = CalculateVacationDaysTotal(employeeEco.VacationDays);

            _context.Entry(employeeEco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEcoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/EmployeeEco/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeEco(Guid id)
        {
            var employeeEco = await _context.EmployeeEcos.FirstOrDefaultAsync(p => p.EmployeeId == id);
            if (employeeEco == null)
            {
                return NotFound();
            }

            _context.EmployeeEcos.Remove(employeeEco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/EmployeeEco/{id}/addSickLeave
        [HttpPost("{id}/addSickLeave")]
        public async Task<IActionResult> AddSickLeave(Guid id, SickLeaveDto sickLeave)
        {
            var employeeEco = await _context.EmployeeEcos.Include(e => e.SickLeaves).FirstOrDefaultAsync(e => e.Id == id);
            if (employeeEco == null)
            {
                return NotFound();
            }

            var convertedSickLeave = new SickLeave
            {
                Id = sickLeave.Id,
                EmployeeEcoId = sickLeave.EmployeeEcoId,
                StartDate = sickLeave.StartDate,
                EndDate = sickLeave.EndDate,
                Reason = sickLeave.Reason
            };

            _context.SickLeaves.Add(convertedSickLeave);

            var daysToAdd = CalculateDaysBetween(sickLeave.StartDate, sickLeave.EndDate);
            employeeEco.SickDaysTotal += daysToAdd;
            employeeEco.LastUpdated = DateTime.Today;
            employeeEco.LastUpdatedBy = sickLeave.UserEmail;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/EmployeeEco/{id}/addVacation
        [HttpPost("{id}/addVacation")]
        public async Task<IActionResult> AddVacation(Guid id, VacationDto vacation)
        {
            var employeeEco = await _context.EmployeeEcos.FindAsync(id);
            if (employeeEco == null)
            {
                return NotFound();
            }

            var convertedVacation = new Vacation
            {
                Id = vacation.Id,
                EmployeeEcoId = vacation.EmployeeEcoId,
                StartDate = vacation.StartDate,
                EndDate = vacation.EndDate
            };

            _context.Vacations.Add(convertedVacation);

            // Update vacation days total
            var daysToAdd = CalculateDaysBetween(vacation.StartDate, vacation.EndDate);

            employeeEco.VacationDaysTotal += daysToAdd;
            employeeEco.LastUpdated = DateTime.Today;
            employeeEco.LastUpdatedBy = vacation.UserEmail;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // Helper function to calculate total days between two dates, including both start and end dates
        private int CalculateDaysBetween(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days + 1;
        }

        private bool EmployeeEcoExists(Guid id)
        {
            return _context.EmployeeEcos.Any(e => e.Id == id);
        }
    }
}
