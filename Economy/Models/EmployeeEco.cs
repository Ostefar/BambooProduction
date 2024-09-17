using System.ComponentModel.DataAnnotations;

namespace Economy.Models
{
    public class EmployeeEco
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Hourly wage is required.")]
        [StringLength(maximumLength: 4, MinimumLength = 3)]
        public string HourlyWage { get; set; }

        public int SickDaysTotal { get; set; }

        public ICollection<SickLeave> SickLeaves { get; set; } = new List<SickLeave>();

        public int VacationDaysTotal { get; set; }

        public ICollection<Vacation> VacationDays { get; set; } = new List<Vacation>();

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        [StringLength(65)]
        public string LastUpdatedBy { get; set; }
    }
}
