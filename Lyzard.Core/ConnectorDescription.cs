using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Core
{

    public enum ConnectorTypes { Input, Output }

    /// <summary>
    /// Provides a description of a connector
    /// </summary>
    public class ConnectorDescription
    {
        /// <summary>
        /// The name of the connector
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The ConnectortType of this connector
        /// </summary>
        public ConnectorTypes ConnectorType { get; internal set; }

        /// <summary>
        /// The delegate to call
        /// </summary>
        public Delegate Delegate { get; internal set; }

        /// <summary>
        /// The value type of this connector
        /// </summary>
        public Type Type { get; internal set; }
    }
}
