using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employees.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Address field is required.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string AddressLine { get; set; }


        [Required(ErrorMessage = "City field is required.")]
        [StringLength(maximumLength: 35, MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip code field is required.")]
        [StringLength(maximumLength: 4, MinimumLength = 4)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Country field is required.")]
        [StringLength(56)]
        public string Country { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(256)]
        public string LastUpdatedBy { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public Address()
        {
            CreatedDate = DateTime.UtcNow;
        }

    }
}
