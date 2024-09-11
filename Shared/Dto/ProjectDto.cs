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

        public int Priority { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }

        public Guid CompanyId { get; set; }

        public CompanyDto Company { get; set; }
    }
}
