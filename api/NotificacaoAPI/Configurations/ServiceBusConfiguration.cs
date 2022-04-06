using Microsoft.Extensions.Azure;
using NotificacaoAPI.Bus;
using NotificacaoAPI.Model;

namespace NotificacaoAPI.Configurations
{
    public static class ServiceBusConfiguration
    {
        public static IServiceCollection AddServiceBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(configuration.GetConnectionString("ServiceBusNotification"));
            });

            services.AddSingleton<IBus<Notification>, BusNotification>();

            return services;
        }
    }
}
