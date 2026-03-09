using System;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Application
{
    public abstract class AService : IMessageDisposable
    {
        public event Action OnDispose;
        protected readonly MessageBus MessageBus;

        protected AService(MessageBus messageBus)
        {
            MessageBus = messageBus;
        }

        public virtual void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}