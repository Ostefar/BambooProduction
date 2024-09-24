using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.EmployeeEco
{
    public class UpdateModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Hourly wage is required.")]
        [StringLength(maximumLength: 4, MinimumLength = 3)]
        public string HourlyWage { get; set; }

        public int SickDaysTotal { get; set; }

        public int VacationDaysTotal { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
