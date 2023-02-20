namespace Report.BackgroundTasks
{
    using Autofac;
    using EventBus;
    using EventBus.Abstractions;
    using EventBusRabbitMQ;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;
    using Report.BackgroundTasks.IntegrationEvents.Events;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    //HostName = Configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };
                factory.UserName = "guest";
                //if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                //{
                //    //factory.UserName = Configuration["EventBusUserName"];

                //}
                factory.Password = "guest";

                //if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                //{
                //    //factory.Password = Configuration["EventBusPassword"];

                //}

                var retryCount = 5;


                return new DefaultRabbitMQPersistentConnection(factory, retryCount);
            });

            RegisterEventBus(services);
            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            ConfigureEventBus(app);
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<EventBus.Abstractions.IEventBus>();

            eventBus.Subscribe<ReportDemandCreatedEvent, IIntegrationEventHandler<ReportDemandCreatedEvent>>();
            
        }

        private void RegisterEventBus(IServiceCollection services)
        {

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;


                return new EventBusRabbitMQ(rabbitMQPersistentConnection, iLifetimeScope, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
            });


            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            //services.AddTransient<ReportDemandCreatedEventHandler>();
        }


    }
}
