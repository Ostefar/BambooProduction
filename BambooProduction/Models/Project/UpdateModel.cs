﻿using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.Project
{
    public class UpdateModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(maximumLength: 200, MinimumLength = 2)]
        public string ProjectDescription { get; set; }

        [Required]
        public PriorityEnum Priority { get; set; }

        [Required]
        public BillingTypeEnum BillingType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public string LastUpdatedBy { get; set; }

    }
}
