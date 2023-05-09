namespace Shared.Messages;

public class SubmitOrder
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
}