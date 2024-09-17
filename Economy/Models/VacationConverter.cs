using Shared.Dto;
using Shared.Interfaces;

namespace Economy.Models
{
    public class VacationConverter : IConverter<Vacation, VacationDto>
    {
        // Converts VacationDto to Vacation model
        public Vacation Convert(VacationDto dto)
        {
            return new Vacation
            {
                Id = dto.Id,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                EmployeeEcoId = dto.EmployeeEcoId
            };
        }

        // Converts Vacation model to VacationDto
        public VacationDto Convert(Vacation model)
        {
            return new VacationDto
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                EmployeeEcoId = model.EmployeeEcoId
            };
        }
    }
}
