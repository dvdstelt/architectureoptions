using Shared.Customers;
using Shared.Interceptors;
using Shared.Messages;

namespace Demo03.StrategicReceiver.Interceptor;

public class StrategicInterceptor : IProperlyForwardMessages<SubmitOrder>
{
    readonly IMessageSession messageSession;

    public StrategicInterceptor(IMessageSession messageSession)
    {
        this.messageSession = messageSession;
    }
    
    public async Task Handle(SubmitOrder order)
    {
        if (!Customers.GetPriorityCustomers().Contains(order.CustomerId))
            return;

        Console.WriteLine($"Sending message to strategic receiver [{order.CustomerId}]");
        
        var sendOptions = new SendOptions();
        sendOptions.SetDestination("StrategicReceiver");

        await messageSession.Send(order, sendOptions);
        
    }
}