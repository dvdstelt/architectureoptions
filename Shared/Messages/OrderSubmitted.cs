namespace Shared.Messages;

public class OrderSubmitted : IEvent
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
}