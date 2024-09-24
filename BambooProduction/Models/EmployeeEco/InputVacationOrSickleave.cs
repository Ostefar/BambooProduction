using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.EmployeeEco
{
    public class InputVacationOrSickleave
    {
        [Required(ErrorMessage = "Start date field is required.")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End date field is required.")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        public string Reason { get; set; }
    }
}
