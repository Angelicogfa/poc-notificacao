using NotificacaoAPI.Bus;

namespace NotificacaoAPI.Configurations
{
    public static class SeviceBusConfiguration
    {
        public static IServiceCollection AddBus(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton<IBus>(t => new Bus.Bus(configuration));
        }
    }
}
