using EventBus.Abstractions;
using Report.BackgroundTasks.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace Report.BackgroundTasks.IntegrationEvents.EventHandling
{
    public class ReportDemandCreatedEventHandler : IIntegrationEventHandler<ReportDemandCreatedEvent>
    {


        public ReportDemandCreatedEventHandler()
        {

        }

        public async Task Handle(ReportDemandCreatedEvent @event)
        {

            int abc = 00;
            Console.WriteLine("KKK");

        }

    }
}
