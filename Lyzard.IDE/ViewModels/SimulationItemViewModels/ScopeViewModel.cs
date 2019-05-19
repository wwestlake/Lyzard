using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Lyzard.CustomControls;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public class ScopeViewModel : SimViewModelBase
    {
        private string _title;
        private TimeSignalDelegate _signalSource;

        public ScopeViewModel()
        {
            Title = "Oscilloscope";
        }

        public IEnumerable<Tuple<double, double>> SignalIn
        {
            get => SignalSource();
        }

        public TimeSignalDelegate SignalSource
        {
            get => _signalSource;
            set
            {
                _signalSource = value;
                OnPropertyChanged();
                OnPropertyChanged("SignalIn");
            }
        }


        public string Title { get => _title; set { _title = value; OnPropertyChanged(); } }

        internal override Delegate ConnectToOutput(string connectorName)
        {
            return null;
        }

        internal override void HandleConnectionAdded(Connector connector)
        {
            if (connector.Name == "VerticalIn")
            {
                foreach (var connection in connector.Connections)
                {
                    if (connection.Source != null)
                    {
                        var vm = (connection.Source.ParentDesignerItem.Content as Control).DataContext as SimViewModelBase;
                        SignalSource = vm.ConnectToOutput(connection.Sink.Name) as TimeSignalDelegate;
                        var list = SignalIn.Take(100).ToList();
                        var a = 1;
                    }
                }
            }
        }
    }
}
