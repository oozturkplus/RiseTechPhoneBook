using System.Collections.Generic;
using System;

namespace Contact.API.Entities
{
    public class Person
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public List<ContactInfo> ContactInfos { get; set; }

    }


}
