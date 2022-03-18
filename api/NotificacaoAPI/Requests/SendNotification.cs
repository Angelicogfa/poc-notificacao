namespace NotificacaoAPI.Requests
{
    public class SendNotification
    {
        public string Sender { get; set; }
        public string Destination { get; set; }
        public string Message { get; set; }
        public string? UrlRedirect { get; set; }
    }
}
