using Microsoft.EntityFrameworkCore;
using NotificacaoAPI.Context;

namespace NotificacaoAPI.Configurations
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotificationContext>(setup => setup.UseSqlServer(configuration.GetConnectionString("ConnDB")));

            services.AddScoped<IUow, UnitOfWork>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            return services;
        }
    }
}
