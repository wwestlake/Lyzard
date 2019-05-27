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