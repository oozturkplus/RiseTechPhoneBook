using Contact.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Services
{
    public interface IReportRepository
    {
        Task AddReportDemandAsync();

        Task<IEnumerable<Report>> GetAllReportsAsync();

        Task<Report> GetReportAsync();
    }
}