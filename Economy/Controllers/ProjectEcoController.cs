using Economy.Data;
using Economy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace Economy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectEcoController : ControllerBase
    {
        private readonly EconomyDbContext _context;
        private HttpClient _projectApiClient;
        private string loggedInUserRole = "Admin";
        private readonly IConverter<ProjectEco, ProjectEcoDto> taskConverter;

        public ProjectEcoController(EconomyDbContext context, IHttpClientFactory httpClientFactory, IConverter<ProjectEco, ProjectEcoDto> converter)
        {
            _context = context;
            _projectApiClient = httpClientFactory.CreateClient("ProjectApi");
            taskConverter = converter;
        }

        // GET: api/ProjectEco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectEcoDto>>> GetProjectEcos([FromQuery] string loggedInUserRole)
        {

            if (loggedInUserRole != "Admin")
            {
                return Unauthorized("You are not allowed to access this resource.");
            }   

            // Call the GetProjectNames API endpoint
            HttpResponseMessage response = await _projectApiClient.GetAsync("Project/names");
            var projectNames = await response.Content.ReadFromJsonAsync<List<ProjectDto>>();

            if (projectNames is not null)
            {
                var projectNameDictionary = projectNames.ToDictionary(p => p.Id, p => p.ProjectName);

                var projectEcos = await _context.ProjectEcos
                    .Select(p => new ProjectEcoDto
                    {
                        Id = p.Id,
                        ProjectName = projectNameDictionary.ContainsKey(p.ProjectId) ? projectNameDictionary[p.ProjectId] : "Unknown Project",
                        TotalCost = p.TotalCost,
                        HoursTotal = p.HoursTotal,
                        MaterialsPriceTotal = p.MaterialsPriceTotal
                    }).ToListAsync();

                return Ok(projectEcos);
            }
            else
            {
                return BadRequest("No projects was found.");
            }
        }


        // GET: api/ProjectEco/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectEcoDto>> GetProjectEco(Guid id)
        {
            var projectEco = await _context.ProjectEcos
               .Where(e => e.Id == id)
               .Select(e => new ProjectEcoDto
               {
                   Id = e.Id // vælg relevante felter

               })
               .FirstOrDefaultAsync();

            if (projectEco == null)
            {
                return NotFound();
            }

            return Ok(projectEco);
        }

        // POST: api/ProjectEco
        [HttpPost]
        public async Task<ActionResult<ProjectEco>> PostProjectEco(ProjectEcoDto projectEco)
        {
            // Convert dto to model
            var convertedProjectEco = taskConverter.Convert(projectEco);

            _context.ProjectEcos.Add(convertedProjectEco);
            await _context.SaveChangesAsync();

            return Ok(convertedProjectEco);
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
            var projectEco = await _context.ProjectEcos.FirstOrDefaultAsync(p => p.ProjectId == id);
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
