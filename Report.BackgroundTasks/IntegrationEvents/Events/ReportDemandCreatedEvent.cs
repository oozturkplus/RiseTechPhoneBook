using EventBus.Events;
using System;

namespace Report.BackgroundTasks.IntegrationEvents.Events
{
    public record ReportDemandCreatedEvent : IntegrationEvent
    {
        public Guid ReportTrackingId { get; private init; }

        public ReportDemandCreatedEvent(Guid reportTrackingId)
        {
            ReportTrackingId = reportTrackingId;
        }
    }
}
