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

        public ICollection<ContactInfo> ContactInfos { get; set; }=new List<ContactInfo>();

    }


}
