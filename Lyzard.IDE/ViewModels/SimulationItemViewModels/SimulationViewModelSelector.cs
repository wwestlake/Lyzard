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
using Lyzard.IDE.UserControls.DiagramControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    internal static class SimulationViewModelSelector
    {
        public static Tuple<ViewModelBase, UserControl> SelectViewAndViewModel(Path item)
        {
            if (item == null) return null;
            if (item.Tag == null) return null;
            if (item.Tag.ToString() == "Function") return new Tuple<ViewModelBase, UserControl>(new SimFunctionViewModel(), new FunctionControl());
            if (item.Tag.ToString() == "Scope") return new Tuple<ViewModelBase, UserControl>(new ScopeViewModel(), new ScopeControl());
            if (item.Tag.ToString() == "Variable") return new Tuple<ViewModelBase, UserControl>(new DoubleVariableViewModel(), new VariableControl());

            return null;
        }
    }
}
