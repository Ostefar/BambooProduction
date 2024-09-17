using Economy.Data;
using Economy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Economy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEcoController : ControllerBase
    {
        private readonly EconomyDbContext _context;

        public EmployeeEcoController(EconomyDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeEco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeEco>>> GetEmployeeEcos()
        {
            return await _context.EmployeeEcos
                .Include(e => e.SickLeaves)
                .Include(e => e.VacationDays)
                .ToListAsync();
        }

        // GET: api/EmployeeEco/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeEco>> GetEmployeeEco(Guid id)
        {
            var employeeEco = await _context.EmployeeEcos
                .Include(e => e.SickLeaves)
                .Include(e => e.VacationDays)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employeeEco == null)
            {
                return NotFound();
            }

            return employeeEco;
        }

        // POST: api/EmployeeEco
        [HttpPost]
        public async Task<ActionResult<EmployeeEco>> PostEmployeeEco(EmployeeEco employeeEco)
        {
            employeeEco.Id = Guid.NewGuid();
            employeeEco.CreatedDate = DateTime.UtcNow;
            employeeEco.LastUpdated = DateTime.UtcNow;

            employeeEco.SickDaysTotal = CalculateSickDaysTotal(employeeEco.SickLeaves);
            employeeEco.VacationDaysTotal = CalculateVacationDaysTotal(employeeEco.VacationDays);

            _context.EmployeeEcos.Add(employeeEco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeEco), new { id = employeeEco.Id }, employeeEco);
        }

        // PUT: api/EmployeeEco/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeEco(Guid id, EmployeeEco employeeEco)
        {
            if (id != employeeEco.Id)
            {
                return BadRequest();
            }

            employeeEco.LastUpdated = DateTime.UtcNow;

            // Recalculate SickDaysTotal and VacationDaysTotal
            employeeEco.SickDaysTotal = CalculateSickDaysTotal(employeeEco.SickLeaves);
            employeeEco.VacationDaysTotal = CalculateVacationDaysTotal(employeeEco.VacationDays);

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
            var employeeEco = await _context.EmployeeEcos.FindAsync(id);
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
        public async Task<IActionResult> AddSickLeave(Guid id, SickLeave sickLeave)
        {
            var employeeEco = await _context.EmployeeEcos.FindAsync(id);
            if (employeeEco == null)
            {
                return NotFound();
            }

            sickLeave.Id = Guid.NewGuid();
            sickLeave.EmployeeEcoId = id;
            employeeEco.SickLeaves.Add(sickLeave);

            // Update sick days total
            employeeEco.SickDaysTotal = CalculateSickDaysTotal(employeeEco.SickLeaves);

            await _context.SaveChangesAsync();

            return Ok(sickLeave);
        }

        // POST: api/EmployeeEco/{id}/addVacation
        [HttpPost("{id}/addVacation")]
        public async Task<IActionResult> AddVacation(Guid id, Vacation vacation)
        {
            var employeeEco = await _context.EmployeeEcos.FindAsync(id);
            if (employeeEco == null)
            {
                return NotFound();
            }

            vacation.Id = Guid.NewGuid();
            vacation.EmployeeEcoId = id;
            employeeEco.VacationDays.Add(vacation);

            // Update vacation days total
            employeeEco.VacationDaysTotal = CalculateVacationDaysTotal(employeeEco.VacationDays);

            await _context.SaveChangesAsync();

            return Ok(vacation);
        }

        // Helper function to calculate total sick days
        private int CalculateSickDaysTotal(ICollection<SickLeave> sickLeaves)
        {
            return sickLeaves.Sum(s => (s.EndDate.HasValue
                ? (s.EndDate.Value - s.StartDate).Days + 1
                : (DateTime.Now - s.StartDate).Days + 1));
        }

        // Helper function to calculate total vacation days
        private int CalculateVacationDaysTotal(ICollection<Vacation> vacations)
        {
            return vacations.Sum(v => (v.EndDate - v.StartDate).Days + 1);
        }

        private bool EmployeeEcoExists(Guid id)
        {
            return _context.EmployeeEcos.Any(e => e.Id == id);
        }
    }
}
