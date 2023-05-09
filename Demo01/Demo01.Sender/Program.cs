using Shared;
using Shared.Configuration;
using Shared.Customers;
using Shared.Messages;

static class Program
{
    const int BatchSize = 250;
    private static IEndpointInstance? endpointInstance;
    private static readonly Random random = new Random();
    private static readonly Guid[] customers = Customers.GetAllCustomers().ToArray();
    
    static async Task Main()
    {
        ConsoleEx.Initialize(batchSize: BatchSize);
        ConsoleEx.DisplayMenuOptions();

        var endpointConfiguration = new EndpointConfiguration("Sender")
            .ApplyDefaultConfiguration();

        endpointInstance = await Endpoint.Start(endpointConfiguration);

        while (true)
        {
            var key = Console.ReadKey(true);
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    await SendMessage();
                    Console.WriteLine($"Messages sent");
                    break;
                case ConsoleKey.D2:
                    await SendBatch();
                    Console.WriteLine($"{BatchSize} messages sent");
                    break;
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
            }
        }
    }
    
    static async Task SendBatch()
    {
        var tasks = new List<Task>();
        var customers = Customers.GetAllCustomers();

        for (int i = 0; i < BatchSize; i++)
        {
            tasks.Add(SendMessage());
        }
        await Task.WhenAll(tasks);
    }

    static async Task SendMessage()
    {
        var customerId = customers[random.Next(customers.Length)];
        var destination = Customers.GetPriorityCustomers().Contains(customerId) ? "StrategicReceiver" : "RegularReceiver";

        var message = new SubmitOrder
        {
            OrderId = Guid.NewGuid(),
            CustomerId = customerId
        };

        var sendOptions = new SendOptions();
        sendOptions.SetDestination(destination);

        await endpointInstance!.Send(message, sendOptions);
    }
 
}

