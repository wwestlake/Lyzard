using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.MessageBus
{
    public delegate void MessageHandler(object sender, Message message);

    public class MessageRegistry : MarshalByRefObject
    {
        private static MessageRegistry _instance;



        private MessageRegistry()
        {

        }

        public static MessageRegistry Instance
        {
            get
            {
                return (_instance ?? (_instance = new MessageRegistry()));
            }
        }

        public void SendMessageTo(object recipient, Message message)
        {

        }

        public void Register(object receiver)
        {

        }

    }
}
