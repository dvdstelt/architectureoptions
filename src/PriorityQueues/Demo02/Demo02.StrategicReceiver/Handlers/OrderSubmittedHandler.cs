using Shared.Customers;
using Shared.Messages;

public class OrderSubmittedHandler : IHandleMessages<OrderSubmitted>
{
    public Task Handle(OrderSubmitted message, IMessageHandlerContext context)
    {
        if (!Customers.GetPriorityCustomers().Contains(message.CustomerId))
            return Task.CompletedTask;

        Console.WriteLine($"Order received for strategic CustomerId [{message.CustomerId}]");

        return Task.CompletedTask;
    }
}