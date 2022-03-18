using NotificacaoAPI.Model;

namespace NotificacaoAPI.Context
{
    public interface INotificationRepository
    {
        Task Add(Notification notification);
        Task<Notification> Update(Notification notification);
        Task<Notification?> Get(Guid id);
        Task<Notification[]> GetByDestiny(string destiny, bool includeRead, int? top);
    }
}
