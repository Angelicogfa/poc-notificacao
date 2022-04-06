namespace NotificacaoAPI.Authentication
{
    public class SingleSignOnEvents
    {
        public Func<SingleSignOnMessageReceivedContext, Task> OnMessageReceived;

        public virtual Task MessageReceived(SingleSignOnMessageReceivedContext context)
        {
            return OnMessageReceived(context);
        }
    }
}
