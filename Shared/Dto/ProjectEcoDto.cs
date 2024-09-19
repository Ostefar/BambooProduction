using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class ProjectEcoDto
    {
        public Guid Id { get; set; }

        public Guid ProjectId { get; set; }

        public String ProjectName { get; set; }

        public int TotalCost { get; set; }

        public int FixedPrice { get; set; }

        public int HoursTotal { get; set; }

        public ICollection<HourDto> Hours { get; set; } = new List<HourDto>();

        public int MaterialsPriceTotal { get; set; }

        public ICollection<MaterialDto> Material { get; set; } = new List<MaterialDto>();

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
