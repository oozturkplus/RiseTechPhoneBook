using System;

namespace Contact.API.Model
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public int ContactInfoTypeId { get; set; }

        public string Info { get; set; }

    }
}
