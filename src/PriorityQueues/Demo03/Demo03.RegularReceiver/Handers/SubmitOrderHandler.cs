using Shared.Configuration;
using Shared.Messages;

namespace Demo03.RegularReceiver.Handers;

public class SubmitOrderHandler : IHandleMessages<SubmitOrder>
{
    public async Task Handle(SubmitOrder message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Message received with CustomerId [{message.CustomerId}]");

        await Task.Delay(Defaults.RegularDelay, context.CancellationToken);
    }
}