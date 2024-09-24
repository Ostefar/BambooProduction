using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class MaterialDto
    {
        public Guid Id { get; set; }

        // skal modificeres senere til at være en dropdown med deres materialer, men jeg har ikke fået de nødvendige oplysninger endnu
        public string MaterialType { get; set; }

        public string Price { get; set; }
        public Guid ProjectEcoId { get; set; }

        public ProjectEcoDto ProjectEco { get; set; }

        public string UserEmail { get; set; }
    }
}
