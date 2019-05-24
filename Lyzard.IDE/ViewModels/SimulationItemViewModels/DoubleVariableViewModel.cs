using Lyzard.CustomControls;
using System;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public class DoubleVariableViewModel : SimViewModelBase
    {
        private string _name;
        private double _value;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public double Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        internal override Delegate ConnectToOutput(Connection connectorName)
        {
            return new DoubleDelegate(() => Value);
        }

        internal override void HandleConnectionAdded(Connector connector)
        {
        }

        internal override void OnDelete()
        {
        }

        internal override void OnDeleteConnection(Connection connection)
        {
        }
    }
}
