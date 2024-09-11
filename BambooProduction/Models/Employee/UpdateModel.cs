using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.Employee
{
    public class UpdateModel
    {
        // Employee-related fields
        [Required]
        [StringLength(50, ErrorMessage = "First name must be between 2 and 50 characters.", MinimumLength = 2)]
        public string FirstName { get; set; } = "";

        [Required]
        [StringLength(45, ErrorMessage = "Last name must be between 2 and 45 characters.", MinimumLength = 2)]
        public string LastName { get; set; } = "";

        [Required]
        [StringLength(8, ErrorMessage = "Phone number must be 8 digits")]
        public string Phone { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required]
        [Range(1, 3)]
        public int JobTitle { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; } = DateTime.Today;

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public string LastUpdatedBy { get; set; }

        // Address-related fields
        [Required]
        [StringLength(100, ErrorMessage = "Address must be between 2 and 100 characters", MinimumLength = 2)]
        public string AddressLine { get; set; } = "";

        [Required]
        [StringLength(168, ErrorMessage = "City must be between 2 and 168 characters", MinimumLength = 2)]
        public string City { get; set; } = "";

        [Required]
        [StringLength(4, ErrorMessage = "Zipcode must be between 4 digits", MinimumLength = 4)]
        public string ZipCode { get; set; } = "";

        [Required]
        [StringLength(56, ErrorMessage = "Country must be between 2 and 56 characters", MinimumLength = 2)]
        public string Country { get; set; } = "";
    }
}
