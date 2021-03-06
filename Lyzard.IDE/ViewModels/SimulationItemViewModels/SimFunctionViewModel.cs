﻿/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Lyzard.CustomControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    internal class SimFunctionViewModel : SimViewModelBase
    {
        private string _title;
        private ObservableCollection<FunctionGenerator> _func = new ObservableCollection<FunctionGenerator>();
        private FunctionGenerator _selection;
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
            set
            {
                if (_selection != value)
                {
                    _selection = value;
                    Title = _selection.Name + " Generator";
                }
                OnPropertyChanged();
                OnSettingsChanged();
            }
        }

        /// <summary>
        /// Sets or Gets the StartTime of the Generator
        /// </summary>
        public double StartTime
        {
            get { return Selection.Generator.StartTime; }
            set
            {
                Selection.Generator.StartTime = value;
                OnPropertyChanged();
                OnSettingsChanged();
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
                StartTimeEnabled = value == null;
                OnPropertyChanged("StartTimeEnabled");
                OnSettingsChanged();
            }
        }

        public bool StartTimeEnabled { get; set; } = true;

        /// <summary>
        /// Sets or Gets the Amplitude of the Generator
        /// </summary>
        public double Amplitude { get { return Selection.Generator.Amplitude; }
            set
            {
                Selection.Generator.Amplitude = value;
                OnPropertyChanged();
                OnSettingsChanged();            }
        }

        public DoubleDelegate AmplitudeSource
        {
            get => _amplitudeSource;
            set
            {
                _amplitudeSource = value;
                OnPropertyChanged();
                OnPropertyChanged("Amplitude");
                AmplitudeEnabled = value == null;
                OnPropertyChanged("AmplitudeEnabled");
                OnSettingsChanged();
            }
        }

        public bool AmplitudeEnabled { get; set; } = true;


        /// <summary>
        /// Sets or Gets the Frequency of the Generator
        /// </summary>
        public double Frequency { get { return Selection.Generator.Frequency; }
            set
            {
                Selection.Generator.Frequency = value;
                OnPropertyChanged();
                OnSettingsChanged();
            }
        }

        public DoubleDelegate FrequencySource
        {
            get => _frequencySource;
            set
            {
                _frequencySource = value;
                OnPropertyChanged();
                OnPropertyChanged("Frequency");
                FrequencyEnabled = value == null;
                OnPropertyChanged("FrequencyEnabled");
                OnSettingsChanged();
            }
        }

        public bool FrequencyEnabled { get; set; } = true;

        /// <summary>
        /// Gets or Sets the SampleRate of the Generator
        /// </summary>
        public double SampleRate { get { return Selection.Generator.SampleRate; }
            set
            {
                Selection.Generator.SampleRate = value;
                OnPropertyChanged();
                OnSettingsChanged();
            }
        }

        public DoubleDelegate SampleRateSource
        {
            get => _sampleRateSource;
            set
            {
                _sampleRateSource = value;
                OnPropertyChanged();
                OnPropertyChanged("SampleRate");
                SampleRateEnabled = value == null;
                OnPropertyChanged("SampleRateEnabled");
                OnSettingsChanged();
            }
        }

        public bool SampleRateEnabled { get; set; } = true;


        /// <summary>
        /// Gets or Sets the Phase of the Generator
        /// </summary>
        public double Phase { get { return Selection.Generator.Phase; }
            set
            {
                Selection.Generator.Phase = value;
                OnPropertyChanged();
                OnSettingsChanged();
            }
        }

        public DoubleDelegate PhaseSource
        {
            get => _phaseSource;
            set
            {
                _phaseSource = value;
                OnPropertyChanged();
                OnPropertyChanged("Phase");
                PhaseEnabled = value == null;
                OnPropertyChanged("PhaseEnabled");
                OnSettingsChanged();
            }
        }

        public bool PhaseEnabled { get; set; } = true;

        protected void OnSettingsChanged()
        {
            foreach (var connection in Connections.Keys)
            {
                var content = connection.Sink.ParentDesignerItem.Content as Control;
                var vm = content.DataContext as SimViewModelBase;
                vm.HandleSettingsChanged(connection);
            }
        }

        private Dictionary<Connection, TimeSignalDelegate> Connections = new Dictionary<Connection, TimeSignalDelegate>();
        private DoubleDelegate _phaseSource;
        private DoubleDelegate _sampleRateSource;
        private DoubleDelegate _frequencySource;
        private DoubleDelegate _amplitudeSource;

        internal override Delegate ConnectToOutput(Connection connection)
        {
            var generator = new FunctionGenerator(_selection.Generator);
            var output = new TimeSignalDelegate(() => generator.Output);
            if (Connections.ContainsKey(connection))
                return output;
            Connections.Add(connection, output);
            return output;
        }

        internal override void HandleConnectionAdded(Connector connector)
        {
            switch (connector.Name)
            {
                case "StartTime":
                    SetDelegate(connector, () => StartTimeSource, StartTimePropertyChanged, () => StartTime);
                    break;
                case "Amplitude":
                    SetDelegate(connector, () => AmplitudeSource, AmplitudePropertyChanged, () => Amplitude);
                    break;
                case "Frequency":
                    SetDelegate(connector, () => FrequencySource, FrequencyPropertyChanged, () => Frequency);
                    break;
                case "SampleRate":
                    SetDelegate(connector, () => SampleRateSource, SampleRatePropertyChanged, () => SampleRate);
                    break;
                case "Phase":
                    SetDelegate(connector, () => PhaseSource, PhasePropertyChanged, () => Phase);
                    break;
            }

        }


        internal override void OnDelete()
        {

        }

        internal override void OnDeleteConnection(Connection connection)
        {
            var connector = connection.Sink;
            switch (connector.Name)
            {
                case "StartTime":
                    RemoveDelegate(connector, () => StartTimeSource, StartTimePropertyChanged, () => StartTime);
                    break;
            }

            if (Connections.ContainsKey(connection))
            {
 
                Connections.Remove(connection);

            }
        }


        private void StartTimePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StartTime = StartTimeSource();
        }


        private void AmplitudePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Amplitude = AmplitudeSource();
        }

        private void FrequencyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Frequency = FrequencySource();
        }

        private void SampleRatePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SampleRate = SampleRateSource();
        }

        private void PhasePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Phase = PhaseSource();
        }

        public override void HandleSettingsChanged(Connection connection)
        {
        }
    }
}
