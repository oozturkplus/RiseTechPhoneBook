using AutoMapper;
using Contact.API.Model;
using Contact.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;

        public ReportController(
            IMapper mapper, IReportRepository reportRepository)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
        }

        //[HttpPost]
        //[Route("createreportdemand")]
        //[ProducesResponseType((int)HttpStatusCode.Created)]
        //public async Task<IEnumerable<PersonDto>> CreateReportDemand()
        //{
        //    //return await _reportRepository.AddReportDemandAsync();
        //    return Ok(new List<PersonDto>());
        //}
    }
}
