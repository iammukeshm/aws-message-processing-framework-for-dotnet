namespace Contracts.Events;
public class OrderCreatedEvent : IEvent
{
    public Guid OrderId { get; }
    public Guid CustomerId { get; }
    public DateTime CreatedDate { get; }
    public OrderCreatedEvent(Guid orderId, Guid customerId)
    {
        this.OrderId = orderId;
        this.CustomerId = customerId;
        CreatedDate = DateTime.UtcNow;
    }
}