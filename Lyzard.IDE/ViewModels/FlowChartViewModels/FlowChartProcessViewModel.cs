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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.FlowChartViewModels
{
    internal class FlowChartProcessViewModel : ViewModelBase
    {
        private string _name;
        private object tag;

        public FlowChartProcessViewModel(object tag)
        {
            this.tag = tag;
            if (tag != null) Name = this.tag.ToString();
        }

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
    }
}
