using System;

namespace Plugins.MessagePipe.MessageBus.Runtime
{
    public static class MessageBusExtensions
    {
        public static void Subscribe<T>(this IMessageDisposable disposable, MessageBus messageBus, Action<T> action)
        {
            messageBus.Subscribe(action, disposable);
        }
    }
}