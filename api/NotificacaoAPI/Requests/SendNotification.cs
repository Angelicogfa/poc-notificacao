namespace NotificacaoAPI.Requests
{
    public class SendNotification
    {
        public SendNotification()
        {
            Horario = DateTime.Now;
        }

        public string Sender { get; set; }
        public string Destination { get; set; }
        public string Message { get; set; }
        public string? UrlRedirect { get; set; }
        public DateTime Horario { get; set; }
    }
}
