namespace NotificacaoAPI.Bus
{
    public interface IBus : IAsyncDisposable
    {
        Task SendAsync(object message, CancellationToken token);
    }

    public interface IBus<TIn> : IBus
    {
        Task StartProcessingWithCallback(Func<TIn, Task> process, CancellationToken token);
    }
    public interface IBus<TIn, TOut> : IBus
    {
        Task StartprocessingWithCallback(Func<TIn, Task<TOut>> process, CancellationToken token);
    }
}
