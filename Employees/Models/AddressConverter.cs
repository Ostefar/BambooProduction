using Shared.Dto;
using Shared.Interfaces;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class AddressConverter : IConverter<Address, AddressDto>
    {
        // Converts AddressDto to Address model
        public Address Convert(AddressDto dto)
        {
            return new Address
            {
                Id = dto.Id,
                AddressLine = dto.AddressLine,
                City = dto.City,
                ZipCode = dto.ZipCode,
                Country = dto.Country,
                CreatedDate = dto.CreatedDate,
                LastUpdated = dto.LastUpdated,
                LastUpdatedBy = dto.LastUpdatedBy
            };
        }

        // Converts Address model to AddressDto
        public AddressDto Convert(Address model)
        {
            return new AddressDto
            {
                Id = model.Id,
                AddressLine = model.AddressLine,
                City = model.City,
                ZipCode = model.ZipCode,
                Country = model.Country,
                CreatedDate = model.CreatedDate,
                LastUpdated = model.LastUpdated,
                LastUpdatedBy = model.LastUpdatedBy
            };
        }
    }
}
