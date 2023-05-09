using Shared.Messages;

namespace Demo01.RegularReceiver.Handlers;

public class SubmitOrderHandler : IHandleMessages<SubmitOrder>
{
    public Task Handle(SubmitOrder message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Message received with CustomerId [{message.CustomerId}]");

        return Task.CompletedTask;
    }
}