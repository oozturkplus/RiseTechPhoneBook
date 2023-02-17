using Contact.API.Entities;
using System;
using System.Collections.Generic;

namespace Contact.API.Model
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public ICollection<ContactInfoDto> ContactInfos { get; set; } = new List<ContactInfoDto>();
    }
}
