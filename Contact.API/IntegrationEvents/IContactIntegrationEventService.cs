using EventBus.Events;
using System.Threading.Tasks;

namespace Contact.API.IntegrationEvents
{
    public interface IContactIntegrationEventService
    {
        Task SaveEventAndContactContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
