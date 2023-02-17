using System;
using System.ComponentModel.DataAnnotations;

namespace Contact.API.Entities
{

    public class ContactInfo
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public int ContactInfoTypeId { get; set; }

        public string Info { get; set;}

        public Person Person { get; set; }
    }

    
}
