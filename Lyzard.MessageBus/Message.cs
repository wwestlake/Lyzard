using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.MessageBus
{
    public abstract class Message
    {
        public Guid Id { get; } = Guid.NewGuid();
    }

    public class LogMessage : Message
    {
        public LogMessage(string message)
        {
            Text = message;
        }

        public string Text { get; private set; }
    }

}
