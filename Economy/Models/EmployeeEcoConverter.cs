using Shared.Dto;
using Shared.Interfaces;

namespace Economy.Models
{
    public class EmployeeEcoConverter : IConverter<EmployeeEco, EmployeeEcoDto>
    {
        // Converts EmployeeEcoDto to EmployeeEco model
        public EmployeeEco Convert(EmployeeEcoDto dto)
        {
            return new EmployeeEco
            {
                Id = dto.Id,
                EmployeeId = dto.EmployeeId,
                HourlyWage = dto.HourlyWage,
                SickDaysTotal = dto.SickDaysTotal,
                VacationDaysTotal = dto.VacationDaysTotal,
                CreatedDate = dto.CreatedDate,
                LastUpdated = dto.LastUpdated,
                LastUpdatedBy = dto.LastUpdatedBy,

            };
        }

        // Converts EmployeeEco model to EmployeeEcoDto
        public EmployeeEcoDto Convert(EmployeeEco model)
        {
            return new EmployeeEcoDto
            {
                Id = model.Id,
                EmployeeId = model.EmployeeId,
                HourlyWage = model.HourlyWage,
                SickDaysTotal = model.SickDaysTotal,
                VacationDaysTotal = model.VacationDaysTotal,
                CreatedDate = model.CreatedDate,
                LastUpdated = model.LastUpdated,
                LastUpdatedBy = model.LastUpdatedBy,
            };
        }
    }
}
