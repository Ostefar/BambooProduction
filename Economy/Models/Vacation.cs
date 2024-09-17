using Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace Economy.Models
{
    public class Vacation
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Start date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        public Guid EmployeeEcoId { get; set; }

        public EmployeeEco EmployeeEco { get; set; }
    }
}
