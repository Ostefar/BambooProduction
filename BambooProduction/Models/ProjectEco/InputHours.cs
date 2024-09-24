using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.ProjectEco
{
    public class InputHours
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Registration date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Hours used is required.")]
        [Range(1, 8, ErrorMessage = "Hours used must be between 1 and 8.")]
        public int HoursUsed { get; set; }

        public string? Comment { get; set; }

        [Required]
        public Guid ProjectEcoId { get; set; }
    }
}
