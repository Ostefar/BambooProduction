using Shared.Dto;
using Shared.Interfaces;

namespace Economy.Models
{
    public class HourConverter : IConverter<Hour, HourDto>
    {
        // Converts HourDto to Hour model
        public Hour Convert(HourDto dto)
    {
        return new Hour
        {
            Id = dto.Id,
            RegistrationDate = dto.RegistrationDate,
            HoursUsed = dto.HoursUsed,
            Comment = dto.Comment,
            ProjectEcoId = dto.ProjectEcoId
        };
    }

        // Converts Hour model to HourDto
        public HourDto Convert(Hour model)
    {
        return new HourDto
        {
            Id = model.Id,
            RegistrationDate = model.RegistrationDate,
            HoursUsed = model.HoursUsed,
            Comment = model.Comment,
            ProjectEcoId = model.ProjectEcoId
        };
    }
    }
}
