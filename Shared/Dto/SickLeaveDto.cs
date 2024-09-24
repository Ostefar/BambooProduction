using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class SickLeaveDto
    {
        public Guid Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string Reason { get; set; }

        public Guid EmployeeEcoId { get; set; }

        public EmployeeEcoDto EmployeeEco { get; set; }

        public string UserEmail { get; set; }

    }
}
