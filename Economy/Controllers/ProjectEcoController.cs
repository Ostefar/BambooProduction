using Economy.Data;
using Economy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Economy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEcoController : ControllerBase
    {
        private readonly EconomyDbContext _context;

        public ProjectEcoController(EconomyDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectEco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectEco>>> GetProjectEcos()
        {
            return await _context.ProjectEcos
                .Include(p => p.Hours)
                .Include(p => p.Material)
                .ToListAsync();
        }

        // GET: api/ProjectEco/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectEco>> GetProjectEco(Guid id)
        {
            var projectEco = await _context.ProjectEcos
                .Include(p => p.Hours)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (projectEco == null)
            {
                return NotFound();
            }

            return projectEco;
        }

        // POST: api/ProjectEco
        [HttpPost]
        public async Task<ActionResult<ProjectEco>> PostProjectEco(ProjectEco projectEco)
        {
            projectEco.Id = Guid.NewGuid();
            projectEco.CreatedDate = DateTime.UtcNow;
            projectEco.LastUpdated = DateTime.UtcNow;

            _context.ProjectEcos.Add(projectEco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectEco), new { id = projectEco.Id }, projectEco);
        }

        // PUT: api/ProjectEco/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectEco(Guid id, ProjectEco projectEco)
        {
            if (id != projectEco.Id)
            {
                return BadRequest();
            }

            projectEco.LastUpdated = DateTime.UtcNow;

            _context.Entry(projectEco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectEcoExists(id))
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

        // DELETE: api/ProjectEco/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectEco(Guid id)
        {
            var projectEco = await _context.ProjectEcos.FindAsync(id);
            if (projectEco == null)
            {
                return NotFound();
            }

            _context.ProjectEcos.Remove(projectEco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Add Hour to ProjectEco
        [HttpPost("{id}/addHour")]
        public async Task<IActionResult> AddHourToProject(Guid id, Hour hour)
        {
            var projectEco = await _context.ProjectEcos.FindAsync(id);
            if (projectEco == null)
            {
                return NotFound();
            }

            hour.Id = Guid.NewGuid();
            hour.ProjectEcoId = id;
            projectEco.Hours.Add(hour);

            await _context.SaveChangesAsync();

            return Ok(hour);
        }

        // Add Material to ProjectEco
        [HttpPost("{id}/addMaterial")]
        public async Task<IActionResult> AddMaterialToProject(Guid id, Material material)
        {
            var projectEco = await _context.ProjectEcos.FindAsync(id);
            if (projectEco == null)
            {
                return NotFound();
            }

            material.Id = Guid.NewGuid();
            material.ProjectEcoId = id;
            projectEco.Material.Add(material);

            await _context.SaveChangesAsync();

            return Ok(material);
        }

        private bool ProjectEcoExists(Guid id)
        {
            return _context.ProjectEcos.Any(e => e.Id == id);
        }
    }
}
