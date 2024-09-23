using Employees.Data;
using Employees.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        private readonly IConverter<Employee, EmployeeDto> taskConverter;

        public EmployeeController(EmployeeDbContext context, IConverter<Employee, EmployeeDto> converter)
        {
            _context = context;
            taskConverter = converter;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees([FromQuery] String loggedInUserRole)
        {
            List<EmployeeDto> employees;

            if (loggedInUserRole == "Admin")
            {
                employees = await _context.Employees
                  .Select(e => new EmployeeDto
                  {
                      Id = e.Id,
                      FirstName = e.FirstName,
                      LastName = e.LastName,
                      Phone = e.Phone,
                      Email = e.Email,
                      LastUpdated = e.LastUpdated,
                      LastUpdatedBy = e.LastUpdatedBy,
                      Address = new AddressDto
                      {
                          City = e.Address.City,
                          Country = e.Address.Country,
                          LastUpdated = e.LastUpdated,
                          LastUpdatedBy = e.LastUpdatedBy,
                      }
                  })
                  .ToListAsync();

                return Ok(employees);
            }
            else
            {
                return Unauthorized("You are not allowed to do this");
            }
        }
        // GET: api/Employee/names
        [HttpGet("names")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeeNames()
        {
            var employeeNames = await _context.Employees
                .Select(p => new EmployeeDto
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName,
                })
                .ToListAsync();

            return Ok(employeeNames);
        }


        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid id, [FromQuery] String loggedInUserRole)
        {
            var employee = new EmployeeDto();

            if (loggedInUserRole == "Admin")
            {
                employee = await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Phone = e.Phone,
                    Email = e.Email,
                    JobTitle = e.JobTitle,
                    UserId = e.UserId,
                    BirthDate = e.BirthDate,
                    HiringDate = e.HiringDate,
                    CreatedDate = e.CreatedDate,
                    LastUpdated = e.LastUpdated,
                    LastUpdatedBy = e.LastUpdatedBy,
                    Address = new AddressDto
                    {
                        AddressLine = e.Address.AddressLine,
                        City = e.Address.City,
                        ZipCode = e.Address.ZipCode,
                        Country = e.Address.Country,
                        CreatedDate = e.CreatedDate,
                        LastUpdated = e.LastUpdated,
                        LastUpdatedBy = e.LastUpdatedBy,
                    }
                })
                .FirstOrDefaultAsync();
            }
            else
            {
                return Unauthorized("You are not allowed to do this");
            }

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDto employee)
        {
            if (employee.LoggedInUserRole == "Admin")
            {
                // er der nogle fields der skal krypteres?
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (employee.UserId == null)
                {
                    return Unauthorized();
                }
                employee.AddressId = Guid.NewGuid();

                // Convert dto to model
                var convertedEmployee = taskConverter.Convert(employee);

                _context.Employees.Add(convertedEmployee);
                await _context.SaveChangesAsync();

                return Ok(employee);
            }
            else 
            {
                return Unauthorized(employee.FirstName + " " + employee.LastName);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, EmployeeDto employee, [FromQuery] String loggedInUserRole)
        {

            if (employee.LoggedInUserRole == "Admin")
            {
                var existingEmployee = await _context.Employees
                    .Include(e => e.Address)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (existingEmployee == null)
                {
                    return NotFound();
                }

                // Update employee details
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Email = employee.Email;
                existingEmployee.JobTitle = employee.JobTitle;
                existingEmployee.BirthDate = employee.BirthDate;
                existingEmployee.HiringDate = employee.HiringDate;
                existingEmployee.LastUpdated = DateTime.UtcNow;
                existingEmployee.LastUpdatedBy = employee.LastUpdatedBy;  

                // Update address details
                existingEmployee.Address.AddressLine = employee.Address.AddressLine;
                existingEmployee.Address.City = employee.Address.City;
                existingEmployee.Address.ZipCode = employee.Address.ZipCode;
                existingEmployee.Address.Country = employee.Address.Country;
                existingEmployee.Address.LastUpdated = DateTime.UtcNow;
                existingEmployee.Address.LastUpdatedBy = employee.LastUpdatedBy;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok("Updated successfully");

            }
            else
            {
                return Unauthorized(employee.FirstName + " " + employee.LastName);
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, String loggedInUserRole)
        {
            if (loggedInUserRole == "Admin")
            {
                var employee = await _context.Employees
                    .Include(e => e.Address)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employee == null)
                {
                    return NotFound("User does not exist");
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else 
            {
                return Unauthorized("You are not allowed to do this");
            }
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        [HttpGet("BirthdaysToday")]
        public IActionResult GetEmployeesWithBirthdayToday()
        {
            var today = DateTime.Today;

            var employeesWithBirthday = _context.Employees
                .Where(e => e.BirthDate.Month == today.Month && e.BirthDate.Day == today.Day)
                .Select(e => new
                {
                    FullName = $"{e.FirstName} {e.LastName}",
                    Age = today.Year - e.BirthDate.Year - (e.BirthDate > today.AddYears(-(today.Year - e.BirthDate.Year)) ? 1 : 0) 
                })
                .ToList();

            if (employeesWithBirthday.Count == 0)
            {
                return Ok(new List<object>());
            }

            return Ok(employeesWithBirthday);
        }
    }
}
