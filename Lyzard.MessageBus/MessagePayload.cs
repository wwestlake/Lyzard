using System;

namespace Lyzard.MessageBus
{
    public class MessagePayload<T>
    {
        public object Sender { get; set; }
        public T Details { get; set; }
        public DateTime TimeStamp { get; set; }

        public MessagePayload(T payload, object source)
        {
            Sender = source;
            Details = payload;
            TimeStamp = DateTime.Now;
        }
    }
}