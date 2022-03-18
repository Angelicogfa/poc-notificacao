using Microsoft.EntityFrameworkCore;
using NotificacaoAPI.Model;

namespace NotificacaoAPI.Context
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext context;
        public NotificationRepository(NotificationContext context)
        {
            this.context = context;
        }

        public async Task Add(Notification notification)
        {
            await context.Notifications.AddAsync(notification);
        }

        public async Task<Notification?> Get(Guid id)
        {
            return await context.Notifications.FindAsync(id);
        }

        public async Task<Notification[]> GetByDestiny(string destiny, bool includeRead, int? top = null)
        {
            var query = context.Notifications.Where(t => t.Destination.ToLower() == destiny.ToLower());
            
            if (!includeRead)
                query = query.Where(t => t.IsRead == false);

            query = query.OrderByDescending(t => t.IssueDate);

            if (top != null)
                query = query.Take(top.Value);

            return await query.ToArrayAsync();
        }

        public Task<Notification> Update(Notification notification)
        {
            return Task.FromResult(context.Notifications.Update(notification).Entity);
        }
    }
}
