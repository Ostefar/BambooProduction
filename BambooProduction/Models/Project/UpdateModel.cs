using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.Project
{
    public class UpdateModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(8)]
        public string Cvr { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ContactPersonFirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ContactPersonLastName { get; set; }

        [Required]
        [StringLength(8)]
        public string ContactPersonPhone { get; set; }

        [Required]
        [StringLength(65, MinimumLength = 2)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ContactPersonEmail { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string AddressLine { get; set; }

        [Required]
        [StringLength(168, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(56)]
        public string Country { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; } = DateTime.Today;

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public string LastUpdatedBy { get; set; }

    }
}
