using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.Employee
{
    public class InputModel
    {
        // Identity-related fields
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";

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
        [Range(1, 3)]
        public int JobTitle { get; set; }

        [Required]
        [Range(1, 2)]
        public int Role { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; } = DateTime.Today;

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

        // EmployeeEco related fields
        [Required]
        [StringLength(4, ErrorMessage = "HourlyWage must be between 3 and 4 characters", MinimumLength = 3)]
        public String HourlyWage { get; set; }
    }
}
