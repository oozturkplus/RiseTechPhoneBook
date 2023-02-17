using System;

namespace Contact.API.Model
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;
    }
}
