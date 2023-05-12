using Shared;
using Shared.Configuration;

ConsoleEx.Initialize();

var endpointConfiguration = new EndpointConfiguration("StrategicReceiver")
    .ApplyDefaultConfiguration();

var endpointInstance = await Endpoint.Start(endpointConfiguration);

Console.WriteLine("Press a key to quit...");
Console.ReadKey(true);

await endpointInstance.Stop();