using AWS.Messaging;
using Contracts.Events;

namespace ServiceB.Handlers;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : IMessageHandler<OrderCreatedEvent>
{
    public Task<MessageProcessStatus> HandleAsync(MessageEnvelope<OrderCreatedEvent> messageEnvelope, CancellationToken token = default)
    {
        logger.LogInformation($"received order created message for order id: {messageEnvelope.Message.OrderId}");
        return Task.FromResult(MessageProcessStatus.Success());
    }
}
