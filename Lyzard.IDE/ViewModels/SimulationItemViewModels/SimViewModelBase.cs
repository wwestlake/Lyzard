using Lyzard.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public abstract class SimViewModelBase : ViewModelBase
    {


        internal abstract void HandleConnectionAdded(Connector connector);
        internal abstract Delegate ConnectToOutput(string connectorName);
    }
}
