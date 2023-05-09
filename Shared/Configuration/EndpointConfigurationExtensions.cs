namespace Shared.Configuration;

public static class EndpointConfigurationExtensions
{
    public static EndpointConfiguration ApplyDefaultConfiguration(this EndpointConfiguration endpointConfiguration)
    {
        endpointConfiguration.UseTransport<LearningTransport>();
        endpointConfiguration.UsePersistence<LearningPersistence>();

        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();
        endpointConfiguration.Recoverability().Immediate(c => c.NumberOfRetries(0));
        endpointConfiguration.Recoverability().Delayed(c => c.NumberOfRetries(0));

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.AuditProcessedMessagesTo("audit");

        return endpointConfiguration;
    }
}