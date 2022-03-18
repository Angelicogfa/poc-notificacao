using Azure.Messaging.ServiceBus;

namespace NotificacaoAPI.Bus
{
    public class Bus : IBus
    {
        private readonly ServiceBusClient client;
        private readonly string defaultqueue;

        public Bus(IConfiguration config)
        {
            client = new ServiceBusClient(config.GetConnectionString("ServiceBusNotification"));
            defaultqueue = config["ServiceBus:QueueNotification"];
        }

        public async Task Send(object message, string? queue = null)
        {
            var sender = client.CreateSender(queue ?? defaultqueue);
            string payload = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            await sender.SendMessageAsync(new ServiceBusMessage(payload));
        }
    }
}
