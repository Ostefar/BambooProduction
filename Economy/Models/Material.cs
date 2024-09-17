using Shared.Dto;
using System.ComponentModel.DataAnnotations;

namespace Economy.Models
{
    public class Material
    {
        [Key]
        public Guid Id { get; set; }

        // skal modificeres senere til at være en dropdown med deres materialer, men jeg har ikke fået de nødvendige oplysninger endnu
        [Required(ErrorMessage = "Material type is required.")]
        [StringLength(maximumLength: 45, MinimumLength = 2)]
        public string MaterialType { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [StringLength(maximumLength: 45, MinimumLength = 2)]
        public string Price { get; set; }

        [Required]
        public Guid ProjectEcoId { get; set; }

        public ProjectEco ProjectEco { get; set; }
    }
}
