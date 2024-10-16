using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projects.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Company name field is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Cvr field is required.")]
        [StringLength(8)]
        public string Cvr { get; set; }

        [Required(ErrorMessage = "Contact person first name field is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string ContactPersonFirstName { get; set; }

        [Required(ErrorMessage = "Contact person last name field is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string ContactPersonLastName { get; set; }

        [Required(ErrorMessage = "Contact persons phone number is required.")]
        [StringLength(8)]
        public string ContactPersonPhone { get; set; }

        [Required(ErrorMessage = "Contact person email field is required.")]
        [StringLength(maximumLength: 65, MinimumLength = 10)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactPersonEmail { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
        [Required]
        [StringLength(256)]
        public string LastUpdatedBy { get; set; }

        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }
    }
}
