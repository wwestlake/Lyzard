using Lyzard.CustomControls;
using Lyzard.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public class SimFunctionViewModel : SimViewModelBase
    {
        private string _title;
        private ObservableCollection<FunctionGenerator> _func = new ObservableCollection<FunctionGenerator>();
        private FunctionGenerator _selection;
        private string _selectedGenerator;
        private DoubleDelegate _startTimeSource;

        public SimFunctionViewModel()
        {
            Title = "Function Generator";
            _func.Add(new SquareWaveGenerator() { Name = "Square Wave" });
            _func.Add(new SineWaveGenerator() { Name = "Sine Wave" });
            _func.Add(new TriangleWaveGenerator() { Name = "Triangle Wave" });
            _func.Add(new RandomWaveGenerator() { Name = "Random" });
            Selection = _func[0];

        }
        public string Title { get { return _title; } set { _title = value; OnPropertyChanged(); } }

        public ObservableCollection<FunctionGenerator> FunctionType { get => _func; set { _func = value; OnPropertyChanged(); } }

        public FunctionGenerator Selection
        {
            get => _selection;
            set {
                if (_selection != value)
                {
                    _selection = value;
                    Title = _selection.Name + " Generator";
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Sets or Gets the StartTime of the Generator
        /// </summary>
        public double StartTime { get { return Selection.Generator.StartTime; }
            set
            {
                Selection.Generator.StartTime = value;
                OnPropertyChanged();
            }
        }


        public DoubleDelegate StartTimeSource
        {
            get => _startTimeSource;
            set
            {
                _startTimeSource = value;
                OnPropertyChanged();
                OnPropertyChanged("StartTime");
            }
        }



        /// <summary>
        /// Sets or Gets the Amplitude of the Generator
        /// </summary>
        public double Amplitude { get { return Selection.Generator.Amplitude; } set { Selection.Generator.Amplitude = value; OnPropertyChanged(); } }

        /// <summary>
        /// Sets or Gets the Frequency of the Generator
        /// </summary>
        public double Frequency { get { return Selection.Generator.Frequency; } set { Selection.Generator.Frequency = value; OnPropertyChanged(); } }


        /// <summary>
        /// Gets or Sets the SampleRate of the Generator
        /// </summary>
        public double SampleRate { get { return Selection.Generator.SampleRate; } set { Selection.Generator.SampleRate = value; OnPropertyChanged(); } }

        /// <summary>
        /// Gets or Sets the Phase of the Generator
        /// </summary>
        public double Phase { get { return Selection.Generator.Phase; } set { Selection.Generator.Phase = value; OnPropertyChanged(); } }

        private Dictionary<Connection, TimeSignalDelegate> Connections = new Dictionary<Connection, TimeSignalDelegate>();


        internal override Delegate ConnectToOutput(Connection connection)
        {
            var generator = new FunctionGenerator(_selection.Generator);
            var output = new TimeSignalDelegate(() => generator.Output);
            Connections.Add(connection, output);
            return output;
        }

        internal override void HandleConnectionAdded(Connector connector)
        {
            Connection current = null;

            if (connector.Name == "StartTime")
            {
                try
                {
                    foreach (var connection in connector.Connections)
                    {
                        if (connection.Source != null)
                        {
                            var vm = (connection.Source.ParentDesignerItem.Content as Control).DataContext as SimViewModelBase;
                            StartTimeSource = vm.ConnectToOutput(connection) as DoubleDelegate;
                            vm.PropertyChanged += StartTimePropertyChanged;
                            StartTime = StartTimeSource();
                        }
                    }
                } catch
                {
                    if (current != null) connector.Connections.Remove(current);
                }
            }

        }

        internal override void OnDelete()
        {
            
        }

        internal override void OnDeleteConnection(Connection connection)
        {
            if (Connections.ContainsKey(connection))
            {
                Connections.Remove(connection);
            }
        }

        private void StartTimePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StartTime = StartTimeSource();
        }
    }
}
