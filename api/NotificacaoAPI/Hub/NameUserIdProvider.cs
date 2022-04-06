using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace NotificacaoAPI.Hub
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}
