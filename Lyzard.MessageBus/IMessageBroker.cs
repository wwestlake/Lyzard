using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.MessageBus
{
    public interface IMessageBroker : IDisposable
    {
        void Publish<T>(object source, T message, Action<T> replyTo = null);
        void Subscribe<T>(Action<MessagePayload<T>> subscription);
        void Unsubscribe<T>(Action<MessagePayload<T>> subscription);
        void Reply<T>(object source, MessagePayload<T> originator, T message);
    }
}
