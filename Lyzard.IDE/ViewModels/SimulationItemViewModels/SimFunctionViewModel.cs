using Lyzard.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public class SimFunctionViewModel : ViewModelBase
    {
        private string _title;
        private ObservableCollection<FunctionGenerator> _func = new ObservableCollection<FunctionGenerator>();
        private FunctionGenerator _selection;
        private string _selectedGenerator;

        public SimFunctionViewModel()
        {
            Title = "Function Generator";
            _func.Add(new SquareWaveGenerator() { Name = "Square Wave" });
            _func.Add(new SineWaveGenerator() { Name = "Sine Wave" });
            _func.Add(new TriangleWaveGenerator() { Name = "Triangle Wave" });
            _func.Add(new RandomWaveGenerator() { Name = "Random" });
            Selection = _func[0];

        }
        public string Title { get { return _title; } set { _title = value; FirePropertyChanged(); } }

        public ObservableCollection<FunctionGenerator> FunctionType { get => _func; set { _func = value; FirePropertyChanged(); } }

        public FunctionGenerator Selection
        {
            get => _selection;
            set {
                if (_selection != value)
                {
                    _selection = value;
                    Title = _selection.Name + " Generator";
                }
                FirePropertyChanged();
            }
        }

        /// <summary>
        /// Sets or Gets the StartTime of the Generator
        /// </summary>
        public double StartTime { get { return Selection.Generator.StartTime; } set { Selection.Generator.StartTime = value; FirePropertyChanged(); } }

        /// <summary>
        /// Sets or Gets the Amplitude of the Generator
        /// </summary>
        public double Amplitude { get { return Selection.Generator.Amplitude; } set { Selection.Generator.Amplitude = value; FirePropertyChanged(); } }

        /// <summary>
        /// Sets or Gets the Frequency of the Generator
        /// </summary>
        public double Frequency { get { return Selection.Generator.Frequency; } set { Selection.Generator.Frequency = value; FirePropertyChanged(); } }


        /// <summary>
        /// Gets or Sets the SampleRate of the Generator
        /// </summary>
        public double SampleRate { get { return Selection.Generator.SampleRate; } set { Selection.Generator.SampleRate = value; FirePropertyChanged(); } }

        /// <summary>
        /// Gets or Sets the Phase of the Generator
        /// </summary>
        public double Phase { get { return Selection.Generator.Phase; } set { Selection.Generator.Phase = value; FirePropertyChanged(); } }

    }
}
