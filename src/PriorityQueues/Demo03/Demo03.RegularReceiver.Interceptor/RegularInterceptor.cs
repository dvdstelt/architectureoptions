using System.Text;
using Shared.Customers;
using Shared.Interceptors;
using Shared.Messages;

namespace Demo03.RegularReceiver.Interceptor;

public class RegularInterceptor : IProperlyForwardMessages<SubmitOrder>
{
    readonly IMessageSession messageSession;

    public RegularInterceptor(IMessageSession messageSession)
    {
        this.messageSession = messageSession;
    }
    
    public async Task Handle(SubmitOrder order)
    {
        if (Customers.GetPriorityCustomers().Contains(order.CustomerId))
            return;

        Console.WriteLine($"Sending message to regular receiver [{order.CustomerId}]");
        
        var sendOptions = new SendOptions();
        sendOptions.SetDestination("RegularReceiver");

        await messageSession.Send(order, sendOptions);
    }
}