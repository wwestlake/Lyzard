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
using Lyzard.IDE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lyzard.IDE.Views.Pane
{
    class PanesStyleSelector : StyleSelector
    {
        public Style ExplorerStyle
        {
            get;
            set;
        }

        public Style FileStyle
        {
            get;
            set;
        }

        public Style ConsoleStyle
        {
            get;
            set;
        }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is ExplorerViewModelBase)
                return ExplorerStyle;

            if (item is DocumentViewModelBase)
                return FileStyle;

            if (item is ConsoleViewModelBase)
                return ConsoleStyle;

            return base.SelectStyle(item, container);
        }
    }
}
