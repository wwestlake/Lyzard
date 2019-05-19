using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lyzard.CustomControls;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public class DoubleVariableViewModel : SimViewModelBase
    {
        private string _name;
        private double _value;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } } 
        public double Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        internal override Delegate ConnectToOutput(string connectorName)
        {
            return new DoubleDelegate(() => Value);
        }

        internal override void HandleConnectionAdded(Connector connector)
        {
        }
    }
}
