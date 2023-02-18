using Contact.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Services
{
    public interface IReportRepository
    {
        Task<Report> AddReportDemandAsync();

        Task<IEnumerable<Report>> GetAllReportsAsync();

        Task<Report> GetReportAsync(Guid reportId);

        Task<bool> SaveChangesAsync();
    }
}