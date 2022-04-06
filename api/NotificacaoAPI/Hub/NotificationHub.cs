using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NotificacaoAPI.Authentication;
using NotificacaoAPI.Context;
using NotificacaoAPI.Model;
using System.Security.Claims;

namespace NotificacaoAPI.Hub
{
    [Authorize(AuthenticationSchemes = SingleSignOnSchemaConstants.SingleSignOnAuthSchema)]
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly INotificationRepository repository;
        public NotificationHub(INotificationRepository repository)
        {
            this.repository = repository;
        }

        public async Task NotificationsToUser(string user, params Notification[] notification)
        {
            await Clients.User(user).SendAsync("notificationsToUser", notification);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "AllUsers");

            string? username = (Context.User?.Identity as ClaimsIdentity)?.FindFirst("name")?.Value ?? "";

            if(username != null)
            {
                var notifications = await repository.GetByDestiny(username, false, 5)!;
                if (notifications.Any())
                    await Clients.Users(username).SendAsync("notificationsToUser", notifications);
            }
            
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "AllUsers");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
