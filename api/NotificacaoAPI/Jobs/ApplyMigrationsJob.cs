using NotificacaoAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace NotificacaoAPI.Jobs
{
    public class ApplyMigrationsJob : IHostedService
    {
        private readonly IServiceProvider provider;
        public ApplyMigrationsJob(IServiceProvider provider)
        {
            this.provider = provider;   
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var context = provider.CreateScope().ServiceProvider.GetRequiredService<NotificationContext>();
            await context.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
