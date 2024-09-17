using Shared.Dto;
using Shared.Interfaces;

namespace Economy.Models
{
    public class SickLeaveConverter : IConverter<SickLeave, SickLeaveDto>
    {
        // Converts SickLeaveDto to SickLeave model
        public SickLeave Convert(SickLeaveDto dto)
        {
            return new SickLeave
            {
                Id = dto.Id,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                EmployeeEcoId = dto.EmployeeEcoId
            };
        }

        // Converts SickLeave model to SickLeaveDto
        public SickLeaveDto Convert(SickLeave model)
        {
            return new SickLeaveDto
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Reason = model.Reason,
                EmployeeEcoId = model.EmployeeEcoId 
            };
        }
    }
}
