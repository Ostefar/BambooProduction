using Shared.Dto;
using Shared.Interfaces;

namespace Employees.Models
{
    public class EmployeeConverter : IConverter<Employee, EmployeeDto>
    {
        // Converts EmployeeDto to Employee model
        public Employee Convert(EmployeeDto dto)
        {
            return new Employee
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                JobTitle = dto.JobTitle,
                UserId = dto.UserId,
                BirthDate = dto.BirthDate,
                HiringDate = dto.HiringDate,
                CreatedDate = dto.CreatedDate,
                LastUpdated = dto.LastUpdated,
                LastUpdatedBy = dto.LastUpdatedBy,
                AddressId = dto.AddressId,
                Address = dto.Address != null ? new AddressConverter().Convert(dto.Address) : null
            };
        }

        // Converts Employee model to EmployeeDto
        public EmployeeDto Convert(Employee model)
        {
            return new EmployeeDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Email = model.Email,
                JobTitle = model.JobTitle,
                UserId = model.UserId,
                BirthDate = model.BirthDate,
                HiringDate = model.HiringDate,
                CreatedDate = model.CreatedDate,
                LastUpdated = model.LastUpdated,
                LastUpdatedBy = model.LastUpdatedBy,
                AddressId = model.AddressId,
                Address = model.Address != null ? new AddressConverter().Convert(model.Address) : null
            };
        }
    }
}
