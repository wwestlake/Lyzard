using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.Core
{
    /// <summary>
    /// Delegate represents an input to an Execution Block
    /// </summary>
    /// <typeparam name="T">The input type</typeparam>
    /// <param name="Item">The item to send</param>
    public delegate T InputConnector<T>();

    /// <summary>
    /// Delegate represents an output from an Execution block
    /// </summary>
    /// <typeparam name="T">The type of output</typeparam>
    /// <returns>Returns the value</returns>
    public delegate T OutputConnector<T>();

    

 
}
