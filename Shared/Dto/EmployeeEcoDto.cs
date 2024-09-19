using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class EmployeeEcoDto
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public String FullName { get; set; }

        public string HourlyWage { get; set; }

        public int SickDaysTotal { get; set; }

        public ICollection<SickLeaveDto> SickLeaves { get; set; } = new List<SickLeaveDto>();

        public int VacationDaysTotal { get; set; }

        public ICollection<VacationDto> VacationDays { get; set; } = new List<VacationDto>();

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
