using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dto
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; }
        public string Cvr { get; set; }
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonPhone { get; set; }

        public string ContactPersonEmail { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }

        public Guid AddressId { get; set; }

        public AddressDto Address { get; set; }
    }
}
