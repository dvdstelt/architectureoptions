using Shared.Messages;

namespace Demo01.RegularReceiver.Handlers;

public class SubmitOrderHandler : IHandleMessages<SubmitOrder>
{
    public async Task Handle(SubmitOrder message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Message received with CustomerId [{message.CustomerId}]");

        await Task.Delay(50, context.CancellationToken);
    }
}