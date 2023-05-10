using Shared.Customers;
using Shared.Messages;

public class OrderSubmittedHandler : IHandleMessages<OrderSubmitted>
{
    public async Task Handle(OrderSubmitted message, IMessageHandlerContext context)
    {
        if (Customers.GetPriorityCustomers().Contains(message.CustomerId))
            return;

        Console.WriteLine($"Order received for regular CustomerId [{message.CustomerId}]");

        await Task.Delay(50, context.CancellationToken);
    }
}