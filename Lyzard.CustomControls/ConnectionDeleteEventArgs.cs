using System;

namespace Lyzard.CustomControls
{
    public class ConnectionDeleteEventArgs : EventArgs
    {
        public Connection Connection { get; internal set; }
    }
}