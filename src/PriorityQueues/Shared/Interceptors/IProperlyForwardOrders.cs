using Shared.Messages;

namespace Shared.Interceptors;

public interface IProperlyForwardMessages<in T> where T : IMessage
{
    Task Handle(T message);
}