using AutoMapper;
using Contact.API.IntegrationEvents;
using Contact.API.IntegrationEvents.Events;
using Contact.API.Model;
using Contact.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IContactIntegrationEventService _contactIntegrationEventService;

        public ReportController(
            IMapper mapper, IReportRepository reportRepository,
            IContactIntegrationEventService contactIntegrationEventService)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _contactIntegrationEventService= contactIntegrationEventService;
        }

        [HttpPost]
        [Route("createreportdemand")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<ReportDto>> CreateReportDemand()
        {
            Guid reportDemandTrackingId=Guid.NewGuid();

            var addedReportDemand= await _reportRepository.AddReportDemandAsync(reportDemandTrackingId);

            var reportDemandCreatedEvent = new ReportDemandCreatedEvent(
                reportDemandTrackingId);

            await _contactIntegrationEventService.SaveEventAndContactContextChangesAsync(reportDemandCreatedEvent);

            await _contactIntegrationEventService.PublishThroughEventBusAsync(reportDemandCreatedEvent);

            return CreatedAtRoute("GetReport",
                 new
                 {
                     reportId = addedReportDemand.Id
                 },
                 addedReportDemand);
        }

        [HttpGet("{reportId}", Name = "GetReport")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ReportDto>> GetReportAsync(
            Guid reportId)
        {

            var report = await _reportRepository
                .GetReportAsync(reportId);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReportDto>(report));
        }

        [HttpGet]
        [Route("reports")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ReportDto>>>
            GetAllReportsAsync()
        {
            var reports = await _reportRepository.GetAllReportsAsync();

            var reportsDto = reports.Select(
                rpt => _mapper.Map<ReportDto>(rpt)
                );

            return Ok(reportsDto);
        }

        //[HttpGet("{reportId}", Name = "GetReportDetail")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult<ReportDto>>
        //    GetReportDetailAsync(
        //    Guid reportId)
        //{
        //    return Ok();

        //    //var report = await _reportRepository
        //    //    .GetReportAsync(reportId);

        //    //if (report == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //return Ok(_mapper.Map<ReportDto>(report));
        //}
    }
}
