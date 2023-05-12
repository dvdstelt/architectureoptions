using NServiceBus.Hosting.Helpers;
using Shared;
using Shared.Configuration;
using Shared.Customers;
using Shared.Interceptors;
using Shared.Messages;

static class Program
{
    const int BatchSize = 250;
    private static IMessageSession messageSession = null!;
    private static readonly Random random = new Random();
    private static readonly Guid[] customers = Customers.GetAllCustomers().ToArray();
    static IEnumerable<Type> interceptors = null!;

    static async Task Main()
    {
        var assemblyScannerResults = new AssemblyScanner().GetScannableAssemblies();
        interceptors =
            from type in assemblyScannerResults.Types
            where !type.IsAbstract
            from interfaceType in type.GetInterfaces()
            where interfaceType.IsGenericType
            where interfaceType.GetGenericTypeDefinition() == typeof(IProperlyForwardMessages<>)            
            select type;

        ConsoleEx.Initialize(batchSize: BatchSize);
        ConsoleEx.DisplayMenuOptions();
        
        var endpointConfiguration = new EndpointConfiguration("Sender")
            .ApplyDefaultConfiguration();

        messageSession = await Endpoint.Start(endpointConfiguration);

        while (true)
        {
            var key = Console.ReadKey(true);
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    await SendMessage();
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

        for (int i = 0; i < BatchSize; i++)
        {
            tasks.Add(SendMessage());
        }
        await Task.WhenAll(tasks);
    }

    static async Task SendMessage()
    {
        var message = new SubmitOrder
        {
            OrderId = Guid.NewGuid(),
            CustomerId = customers[random.Next(customers.Length)]
        };

        foreach (var interceptor in interceptors)
        {
            var interceptorInstance = Activator.CreateInstance(interceptor, messageSession)!;
            var methods = from m in interceptor.GetMethods()
                where m.Name == "Handle"
                from p  in m.GetParameters()
                where p.ParameterType == message.GetType()
                select m;

            // Invoke the `Handle` method with the parameter
            var methodInfo = methods.Single();
            await (((Task)methodInfo.Invoke(interceptorInstance, new object[] { message })!)!);
        }
    }
 
}

