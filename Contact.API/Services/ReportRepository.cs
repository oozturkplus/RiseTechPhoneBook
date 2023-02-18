using Contact.API.Entities;
using Contact.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Services
{
    public class ReportRepository : IReportRepository
    {
        private readonly ContactContext _context;

        public ReportRepository(ContactContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddReportDemandAsync()
        {
            Report newReport = new Report();
            
            await _context.Report.AddAsync(newReport);

        }

        public Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Report> GetReportAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
