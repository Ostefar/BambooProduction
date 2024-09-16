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
    // [Authorize(Roles = "Admin")]
    public class CustomerController : ControllerBase
    {
        private readonly ProjectDbContext _context;
        private readonly IConverter<Customer, CustomerDto> customerConverter;

        public CustomerController(ProjectDbContext context, IConverter<Customer, CustomerDto> converter)
        {
            _context = context;
            customerConverter = converter;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers([FromQuery] string loggedInUserRole)
        {
            if (loggedInUserRole != "Admin")
            {
                return Unauthorized("You are not allowed to access this resource.");
            }

            var customers = await _context.Customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    Cvr = c.Cvr,
                    ContactPersonFirstName = c.ContactPersonFirstName,
                    ContactPersonLastName = c.ContactPersonLastName,
                    ContactPersonPhone = c.ContactPersonPhone,
                    ContactPersonEmail = c.ContactPersonEmail,
                    CreatedDate = c.CreatedDate,
                    LastUpdated = c.LastUpdated,
                    LastUpdatedBy = c.LastUpdatedBy,
                    Address = new AddressDto
                    {
                        AddressLine = c.Address.AddressLine,
                        City = c.Address.City,
                        ZipCode = c.Address.ZipCode,
                        Country = c.Address.Country,
                        CreatedDate = c.Address.CreatedDate,
                        LastUpdated = c.Address.LastUpdated,
                        LastUpdatedBy = c.Address.LastUpdatedBy
                    }
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id, [FromQuery] string loggedInUserRole)
        {
            if (loggedInUserRole != "Admin")
            {
                return Unauthorized("You are not allowed to access this resource.");
            }

            var customer = await _context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    CompanyName = c.CompanyName,
                    Cvr = c.Cvr,
                    ContactPersonFirstName = c.ContactPersonFirstName,
                    ContactPersonLastName = c.ContactPersonLastName,
                    ContactPersonPhone = c.ContactPersonPhone,
                    ContactPersonEmail = c.ContactPersonEmail,
                    CreatedDate = c.CreatedDate,
                    LastUpdated = c.LastUpdated,
                    LastUpdatedBy = c.LastUpdatedBy,
                    Address = new AddressDto
                    {
                        AddressLine = c.Address.AddressLine,
                        City = c.Address.City,
                        ZipCode = c.Address.ZipCode,
                        Country = c.Address.Country,
                        CreatedDate = c.Address.CreatedDate,
                        LastUpdated = c.Address.LastUpdated,
                        LastUpdatedBy = c.Address.LastUpdatedBy
                    }
                })
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDto customer)
        {
            if (customer.LoggedInUserRole != "Admin")
            {
                return Unauthorized("You are not allowed to create a customer.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customer.Id = Guid.NewGuid();
            customer.AddressId = Guid.NewGuid();

            // Convert DTO to Customer entity
            var convertedCustomer = customerConverter.Convert(customer);

            _context.Customers.Add(convertedCustomer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerDto customer, [FromQuery] string loggedInUserRole)
        {
            if (customer.LoggedInUserRole != "Admin")
            {
                return Unauthorized("You are not allowed to update this customer.");
            }

            var existingCustomer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update customer details
            existingCustomer.CompanyName = customer.CompanyName;
            existingCustomer.Cvr = customer.Cvr;
            existingCustomer.ContactPersonFirstName = customer.ContactPersonFirstName;
            existingCustomer.ContactPersonLastName = customer.ContactPersonLastName;
            existingCustomer.ContactPersonPhone = customer.ContactPersonPhone;
            existingCustomer.ContactPersonEmail = customer.ContactPersonEmail;
            existingCustomer.LastUpdated = DateTime.UtcNow;
            existingCustomer.LastUpdatedBy = customer.LastUpdatedBy;

            // Update address details
            existingCustomer.Address.AddressLine = customer.Address.AddressLine;
            existingCustomer.Address.City = customer.Address.City;
            existingCustomer.Address.ZipCode = customer.Address.ZipCode;
            existingCustomer.Address.Country = customer.Address.Country;
            existingCustomer.Address.LastUpdated = DateTime.UtcNow;
            existingCustomer.Address.LastUpdatedBy = customer.LastUpdatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Customer updated successfully");
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id, [FromQuery] string loggedInUserRole)
        {
            if (loggedInUserRole != "Admin")
            {
                return Unauthorized("You are not allowed to delete this customer.");
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound("Customer does not exist.");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(c => c.Id == id);
        }
    }
}
