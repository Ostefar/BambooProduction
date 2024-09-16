using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public PriorityEnum Priority { get; set; }

        public BillingTypeEnum BillingType { get; set; }

        public string LoggedInUserRole { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }

        public Guid CustomerId { get; set; }
        
        public string CopmpanyName { get; set; }

        public Guid EmployeeId { get; set; }

        public string EmployeeFullName { get; set; }


    }
}
