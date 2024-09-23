using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.Data;
using Projects.Models;
using Shared.Dto;
using Shared.Interfaces;

namespace Projects.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IConverter<Project, ProjectDto> projectConverter;

        public ProjectController(ProjectDbContext context, IConverter<Project, ProjectDto> converter)
        {
            _context = context;
            projectConverter = converter;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var projects = await _context.Projects
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.ProjectDescription,
                    Priority = p.Priority,
                    BillingType = p.BillingType,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CustomerId = p.CustomerId,
                    EmployeeId = p.EmployeeId,
                    CreatedDate = p.CreatedDate,
                    LastUpdated = p.LastUpdated,
                    LastUpdatedBy = p.LastUpdatedBy,
                })
                .ToListAsync();

            return Ok(projects);
        }

        // GET: api/Project/names
        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectNames()
        {
            var projectNames = await _context.Projects
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                })
                .ToListAsync();

            return Ok(projectNames);
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
        {
            var project = await _context.Projects
                .Where(p => p.Id == id)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    ProjectDescription = p.ProjectDescription,
                    Priority = p.Priority,
                    BillingType = p.BillingType,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    CustomerId = p.CustomerId,
                    EmployeeId = p.EmployeeId,
                    CreatedDate = p.CreatedDate,
                    LastUpdated = p.LastUpdated,
                    LastUpdatedBy = p.LastUpdatedBy
                    
                })
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(ProjectDto project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var convertedProject = projectConverter.Convert(project);

            _context.Projects.Add(convertedProject);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(Guid id, ProjectDto project)
        {
            var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProject == null)
            {
                return NotFound();
            }

            // Update project details
            existingProject.ProjectName = project.ProjectName;
            existingProject.ProjectDescription = project.ProjectDescription;
            existingProject.Priority = project.Priority;
            existingProject.BillingType = project.BillingType;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.LastUpdated = DateTime.UtcNow;
            existingProject.LastUpdatedBy = project.LastUpdatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Project updated successfully");
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound("Project does not exist.");
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Project/employeeOnProject/{employeeId}
        [HttpGet("employeeOnProject/{employeeId}")]
        public async Task<ActionResult<bool>> CheckProjectExistsByEmployeeId(Guid employeeId)
        {
            var exists = await _context.Projects.AnyAsync(p => p.EmployeeId == employeeId);

            return Ok(exists);
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(p => p.Id == id);
        }
    }

}
