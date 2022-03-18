namespace NotificacaoAPI.Context
{
    public class UnitOfWork : IUow
    {
        private readonly NotificationContext context;
        private Lazy<INotificationRepository> notification;

        public UnitOfWork(NotificationContext context)
        {

            this.context = context;
            notification = new Lazy<INotificationRepository>(new NotificationRepository(context));
        }

        public INotificationRepository Notifications => notification.Value;

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}
