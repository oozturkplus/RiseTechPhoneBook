using EventBus.Events;
using System;

namespace Contact.API.IntegrationEvents.Events
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
