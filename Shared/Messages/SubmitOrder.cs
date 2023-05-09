namespace Shared.Messages;

public class SubmitOrder : ICommand
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
}