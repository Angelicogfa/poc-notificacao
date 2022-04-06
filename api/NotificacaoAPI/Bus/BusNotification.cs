using Azure.Messaging.ServiceBus;
using NotificacaoAPI.Model;

namespace NotificacaoAPI.Bus
{
    public class BusNotification : Bus, IBus<Notification>
    {
        private Lazy<ServiceBusProcessor> Processor { get; }

        public BusNotification(ServiceBusClient client, ILoggerFactory factory, IConfiguration configuration) : base(client, factory)
        {
            Queue = configuration["ServiceBus:QueueNotification"];
            Processor = new Lazy<ServiceBusProcessor>(() => Client.CreateProcessor(Queue, new ServiceBusProcessorOptions
            {
                PrefetchCount = 10,
                MaxConcurrentCalls = 2,
                AutoCompleteMessages = false,
                ReceiveMode = ServiceBusReceiveMode.PeekLock,
                MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(2),
            }));
        }
       
        public async Task StartProcessingWithCallback(Func<Notification, Task> process, CancellationToken token)
        {
            token.Register(async () =>
            {
                if (Processor.Value.IsProcessing)
                    await Processor.Value.StopProcessingAsync();
            });

            Processor.Value.ProcessMessageAsync += async args => await ProcessMessageAsync(process, args, token);
            Processor.Value.ProcessErrorAsync += Value_ProcessErrorAsync;
            await Processor.Value.StartProcessingAsync(token);
        }

        private Task Value_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            Logger.LogError(arg.Exception, "Unexpected Error on Invoice Message");
            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(Func<Notification, Task> process, ProcessMessageEventArgs args, CancellationToken token)
        {
            try
            {
                var message = args.Message.Body.ToObjectFromJson<Notification>();
                await process(message);
                await args.CompleteMessageAsync(args.Message, token);
            }
            catch (Exception ex)
            {
                await args.DeadLetterMessageAsync(args.Message, "NotificationFailured", ex.InnerException?.Message ?? ex.Message, token);
            }
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            if (Processor.IsValueCreated)
                await Processor.Value.DisposeAsync();
        }
    }
}
