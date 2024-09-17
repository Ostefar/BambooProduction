using Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace Economy.Models
{
    public class SickLeave
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Start date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Reason { get; set; }

        [Required]
        public Guid EmployeeEcoId { get; set; }

        public EmployeeEco EmployeeEco { get; set; }
    }
}
