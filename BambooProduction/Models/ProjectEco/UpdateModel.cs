using System.ComponentModel.DataAnnotations;

namespace BambooProduction.Models.ProjectEco
{
    public class UpdateModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public int TotalCost { get; set; }

        public int FixedPrice { get; set; }

        public int HoursTotal { get; set; }


        public int MaterialsPriceTotal { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
