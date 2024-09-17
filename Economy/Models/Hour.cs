using Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace Economy.Models
{
    public class Hour
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Registration date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "Hours used is required.")]
        [Range(1, 8, ErrorMessage = "Hours used must be between 1 and 8.")]
        public int HoursUsed { get; set; }

        [StringLength(maximumLength: 45, MinimumLength = 2)]
        public string? Comment { get; set; }

        [Required]
        public Guid ProjectEcoId { get; set; }

        public ProjectEcoDto ProjectEco { get; set; }
    }
}
