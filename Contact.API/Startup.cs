using Autofac;
using Contact.API.Infrastructure;
using Contact.API.IntegrationEvents;
using Contact.API.Services;
using EventBus.Abstractions;
using EventBus;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;
using RabbitMQ.Client;

namespace Contact.API
{
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contact.API", Version = "v1" });
            });

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
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory,  retryCount);
            });

            RegisterEventBus(services);

            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IContactIntegrationEventService, ContactIntegrationEventService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<ContactContext>(
                dbContextOptions => dbContextOptions.UseNpgsql(
                    Configuration["ConnectionStrings:RisePhoneBookPostgresDBConnStr"]));

            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses 
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            
            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, iLifetimeScope, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
            });
            

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            //services.AddTransient<ReportDemandCreatedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
