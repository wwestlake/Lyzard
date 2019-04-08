using System;

namespace Lyzard.MessageBus
{
    public class MessagePayload<T>
    {
        public object Sender { get; set; }
        public T Details { get; set; }
        public DateTime TimeStamp { get; set; }

        internal Action<T> ReplyTo { get; set; }

        public MessagePayload(T payload, object source, Action<T> replyTo = null)
        {
            Sender = source;
            Details = payload;
            TimeStamp = DateTime.Now;
            ReplyTo = replyTo;
        }
    }
}