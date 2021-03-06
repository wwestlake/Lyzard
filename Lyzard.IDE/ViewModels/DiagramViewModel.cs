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
using System.Collections.Generic;

namespace Lyzard.IDE.ViewModels
{
    internal class JsonDiagram
    {
        public IEnumerable<DesignerItem> Items { get; set; }
        public IEnumerable<Connection> Connections { get; set; }
    }

    internal class DiagramViewModel : DocumentViewModelBase
    {
        private DesignerCanvas _designer;

        public void RegisterDesigner(DesignerCanvas designer)
        {
            _designer = designer;
        }

        public override bool CanSave(object param)
        {
            return true;
        }

        public override void Close()
        {

        }

        public override void Save(object param)
        {


        }



        public override void SaveAs(object param)
        {
        }
    }
}
