namespace NotificacaoAPI.Bus
{
    public interface IBus
    {
        Task Send(object message, string? queue = null);
    }
}
