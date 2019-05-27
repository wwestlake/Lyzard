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
using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lyzard.IDE.ViewModels.Plugins
{
    internal class UserPluginDocumentViewModel : DocumentViewModelBase
    {
        private IPluginDocumentView _userControl;
        private IPluginDocumentViewModel _viewModel;

        public IPluginDocumentView Content
        {
            get { return _userControl; }
            set
            {
                _userControl = value;
                OnPropertyChanged();
            }
        }

        public IPluginDocumentViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged();
            }
        }

        public new string Title { get { return _viewModel.Title; } set { } }

        public override bool CanSave(object param)
        {
            return _viewModel.CanSave(param);
        }

        public override void Close()
        {
            _viewModel.Close();
        }

        public override void Save(object param)
        {
            _viewModel.Save(param);
        }

        public override void SaveAs(object param)
        {
            _viewModel.SaveAs(param);
        }
    }
}
