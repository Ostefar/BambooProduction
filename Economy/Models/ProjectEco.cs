using System.ComponentModel.DataAnnotations;

namespace Economy.Models
{
    public class ProjectEco
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public int TotalCost { get; set; }

        public int FixedPrice { get; set; }

        public int HoursTotal { get; set; }

        public ICollection<Hour> Hours { get; set; } = new List<Hour>();

        public int MaterialsPriceTotal { get; set; }

        public ICollection<Material> Material { get; set; } = new List<Material>();

        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        [StringLength(65)]
        public string LastUpdatedBy { get; set; }
    }
}
