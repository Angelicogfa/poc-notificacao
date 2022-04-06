using Microsoft.AspNetCore.SignalR;
using NotificacaoAPI.Bus;
using NotificacaoAPI.Hub;
using NotificacaoAPI.Model;

namespace NotificacaoAPI.Jobs
{
    public class NotifyUserToMessageJob : BackgroundService
    {
        private readonly IBus<Notification> bus;
        private readonly IHubContext<NotificationHub> hub;

        public NotifyUserToMessageJob(IBus<Notification> bus, IHubContext<NotificationHub> hub)
        {
            this.bus = bus;
            this.hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await bus.StartProcessingWithCallback(async notification => await NotifyUser(notification, stoppingToken), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }

        private async Task NotifyUser(Notification notification, CancellationToken stoppingToken)
        {
            await hub.Clients.User(notification.Destination).SendAsync("notificationsToUser", new[] { notification }, stoppingToken);
        }
    }
}
