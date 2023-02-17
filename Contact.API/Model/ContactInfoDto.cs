using Contact.API.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contact.API.Model
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public ContactInfoType ContactInfoType { get; set; }

        public string Info { get; set; }

    }
}
