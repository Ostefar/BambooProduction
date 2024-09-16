using Shared.Dto;
using Shared.Interfaces;

namespace Projects.Models
{
    public class CustomerConverter : IConverter<Customer, CustomerDto>
    {
        // Converts CustomerDto to Customer model
        public Customer Convert(CustomerDto dto)
        {
            return new Customer
            {
                Id = dto.Id,
                CompanyName = dto.CompanyName,
                Cvr = dto.Cvr,
                ContactPersonFirstName = dto.ContactPersonFirstName,
                ContactPersonLastName = dto.ContactPersonLastName,
                ContactPersonPhone = dto.ContactPersonPhone,
                ContactPersonEmail = dto.ContactPersonEmail,
                CreatedDate = dto.CreatedDate,
                LastUpdated = dto.LastUpdated,
                LastUpdatedBy = dto.LastUpdatedBy,
                AddressId = dto.AddressId,
                Address = dto.Address != null ? new AddressConverter().Convert(dto.Address) : null

            };
        }

        // Converts Customer model to CustomerDto
        public CustomerDto Convert(Customer model)
        {
            return new CustomerDto
            {
                Id = model.Id,
                CompanyName = model.CompanyName,
                Cvr = model.Cvr,
                ContactPersonFirstName = model.ContactPersonFirstName,
                ContactPersonLastName = model.ContactPersonLastName,
                ContactPersonPhone = model.ContactPersonPhone,
                ContactPersonEmail = model.ContactPersonEmail,
                CreatedDate = model.CreatedDate,
                LastUpdated = model.LastUpdated,
                LastUpdatedBy = model.LastUpdatedBy,
                AddressId = model.AddressId,
                Address = model.Address != null ? new AddressConverter().Convert(model.Address) : null
            };
        }
    }
}
