using Contact.API.Infrastructure;
using EventBus.Abstractions;
using EventBus.Events;
using EventBus.Utilities;
using System;
using System.Threading.Tasks;

namespace Contact.API.IntegrationEvents
{
    public class ContactIntegrationEventService : IContactIntegrationEventService, IDisposable
    {

        private readonly IEventBus _eventBus;
        private readonly ContactContext _contactContext;

        public ContactIntegrationEventService(
            IEventBus eventBus,
            ContactContext contactContext)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
                _eventBus.Publish(evt);
 
        }

        public async Task SaveEventAndContactContextChangesAsync(IntegrationEvent evt)
        {

            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_contactContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _contactContext.SaveChangesAsync();
            });
        }

        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
