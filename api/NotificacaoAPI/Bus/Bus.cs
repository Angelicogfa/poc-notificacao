using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace NotificacaoAPI.Bus
{
    public abstract class Bus : IBus
    {
        public string Queue { get; protected set; }
        protected ILogger<IBus> Logger { get; }
        protected ServiceBusClient Client { get; }
        private Lazy<ServiceBusSender> Sender { get; }

        public Bus(ServiceBusClient client, ILoggerFactory factory)
        {
            Client = client;
            Logger = factory.CreateLogger<Bus>();
            Sender = new Lazy<ServiceBusSender>(() => client.CreateSender(Queue));
        }

        public async Task SendAsync(object message, CancellationToken token)
        {
            var messageJsonBody = JsonSerializer.Serialize(message);
            var azureMessage = new ServiceBusMessage(messageJsonBody)
            {
                MessageId = messageJsonBody.GetHashCode().ToString()
            };

            await Sender.Value.SendMessageAsync(azureMessage, token);
        }

        public virtual async ValueTask DisposeAsync()
        {
            if (Sender.IsValueCreated)
                await Sender.Value.DisposeAsync();
        }
    }
}
