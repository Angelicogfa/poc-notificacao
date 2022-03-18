namespace NotificacaoAPI.Context
{
    public interface IUow
    {
        INotificationRepository Notifications { get; }
        Task Commit();
    }
}
