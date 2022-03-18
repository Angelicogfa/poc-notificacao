namespace NotificacaoAPI.Model
{
    public class Notification
    {
        public Notification(string message, string destination, string sender, string? urlRedirect)
        {
            Id = Guid.NewGuid();
            IssueDate = DateTime.Now;

            Message = message;
            Destination = destination;
            Sender = sender;
            UrlRedirect = urlRedirect;
            IsRead = false;
        }

        public Guid Id { get; private set; }
        public DateTime IssueDate { get; private set; }
        public string Sender { get; private set; }
        public string Destination { get; private set; }
        public string Message { get; private set; }
        public string? UrlRedirect { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadDate { get; private set; }

        public void SetAsRead()
        {
            IsRead = true;
            ReadDate = DateTime.Now;
        }
    }
}
