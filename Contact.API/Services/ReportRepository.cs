using Contact.API.Entities;
using Contact.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Report> AddReportDemandAsync()
        {
            Report newReport = new Report();

            newReport.DemandDateUtc = DateTime.UtcNow;
            newReport.Status = (int)ReportStatus.Waiting;

            await _context.Report.AddAsync(newReport);

            return newReport;

        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _context.Report.OrderBy(c => c.DemandDateUtc)
                .ToListAsync();
        }

        public async Task<Report?> GetReportAsync(Guid reportId)
        {
            return await _context.Report
                      .Where(c => c.Id == reportId)
                      .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
