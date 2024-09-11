using Shared.Dto;
using Shared.Interfaces;

namespace Projects.Models
{
    public class CompanyConverter : IConverter<Company, CompanyDto>
    {
        // Converts CompanyDto to Company model
        public Company Convert(CompanyDto dto)
        {
            return new Company
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

        // Converts Company model to CompanyDto
        public CompanyDto Convert(Company model)
        {
            return new CompanyDto
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
