using NotificacaoAPI.Hub;

namespace NotificacaoAPI.Configurations
{
    public static class SignalRConfiguration
    {
        public static IServiceCollection AddSignalRConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR().AddJsonProtocol()
                .AddAzureSignalR(configuration.GetConnectionString("SignalRNotification"));
            services.AddSingleton((a) => new NotificationHub());

            services.AddCors(opt => 
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}
