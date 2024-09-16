using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projects.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Project name field is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Project description field is required.")]
        [StringLength(maximumLength: 200, MinimumLength = 2)]
        public string ProjectDescription { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [Range(1, 3, ErrorMessage = "Priority must be between 1 and 3.")]
        public int Priority { get; set; }

        [Required(ErrorMessage = "Billing type is required.")]
        [Range(1, 2, ErrorMessage = "Billing type must be between 1 and 2.")]
        public int BillingType { get; set; }

        [Required(ErrorMessage = "Start date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date field is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        [StringLength(256)]
        public string LastUpdatedBy { get; set; }

        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
