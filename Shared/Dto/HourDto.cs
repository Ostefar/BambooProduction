using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class HourDto
    {
        public Guid Id { get; set; }
        public DateTime RegistrationDate { get; set; }

        public int HoursUsed { get; set; }

        public string Comment { get; set; }

        public Guid ProjectEcoId { get; set; }

        public ProjectEcoDto ProjectEco { get; set; }
    }
}
