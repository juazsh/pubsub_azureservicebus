using Azure.Messaging.ServiceBus;

namespace ASBPubSubService.Services;

public class PublishingService(string connectionString)
{
    private readonly string _queueName = "asbqueue";

    public async Task PublishAsync(string message)
    {
        await using var client = new ServiceBusClient(connectionString);
        ServiceBusSender sender = client.CreateSender(_queueName);
        
        ServiceBusMessage messageToPublish = new ServiceBusMessage(message);
        await sender.SendMessageAsync(messageToPublish);
        Console.WriteLine($"Publishing message: {message}");
    }
}