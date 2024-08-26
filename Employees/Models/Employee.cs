using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employees.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name field is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last field is required.")]
        [StringLength(maximumLength: 45, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(8)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [StringLength(maximumLength: 65, MinimumLength = 2)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Job title is required.")]
        [Range(1, 3, ErrorMessage = "Job title must be between 1 and 3.")]
        public int JobTitle { get; set; }

        [Required]
        [StringLength(36)] // 32 hvis det uden -
        public string UserId { get; set; }

        [Required(ErrorMessage = "Birth date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Hiring date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HiringDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(256)]
        public string LastUpdatedBy { get; set; }

        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public Employee()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
