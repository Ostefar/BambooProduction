using System;

namespace Shared
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int JobTitle { get; set; }
        public string UserId { get; set; }

        public string LoggedInUserRole { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
        public Guid AddressId { get; set; }

        public AddressDto Address { get; set; }

    }
}
