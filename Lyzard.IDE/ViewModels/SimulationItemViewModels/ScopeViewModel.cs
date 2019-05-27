/* 
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
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

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

        internal override Delegate ConnectToOutput(Connection connectorName)
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
                        SignalSource = vm.ConnectToOutput(connection) as TimeSignalDelegate;
                        //var test = SignalSource().Take(10).ToList();
                        //var a = 1;
                    }
                }
            }
        }

        internal override void OnDelete()
        {
            SignalSource = null;
        }

        internal override void OnDeleteConnection(Connection connection)
        {
            SignalSource = null;
        }
    }
}
