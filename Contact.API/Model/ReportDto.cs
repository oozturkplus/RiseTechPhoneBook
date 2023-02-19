﻿using System;

namespace Contact.API.Model
{
    public class ReportDto
    {
        public Guid ReportTrackingId { get; set; }
        public DateTime DemandDateUtc { get; set; }

        public int Status { get; set; }

        public string FilePath { get; set; }
    }
}
