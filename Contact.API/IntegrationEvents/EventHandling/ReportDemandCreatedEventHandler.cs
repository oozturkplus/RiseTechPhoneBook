using Contact.API.IntegrationEvents.Events;
using Contact.API.Services;
using EventBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace Contact.API.IntegrationEvents.EventHandling
{

    public class ReportDemandCreatedEventHandler : IIntegrationEventHandler<ReportDemandCreatedEvent>
    {
        private readonly IReportRepository _repository;

        public ReportDemandCreatedEventHandler(
            IReportRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(ReportDemandCreatedEvent @event)
        {
            
            
            
        }

    }
}
