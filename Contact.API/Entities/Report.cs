using System;

namespace Contact.API.Entities
{
    public class Report
    {
        public Guid Id { get; set; }

        public DateTime DemandDate { get; set; }

        public int Status { get; set; }

        public string FilePath { get; set; }
    }
}
