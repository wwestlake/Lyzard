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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels
{



    public class PropertiesViewModel : ExplorerViewModelBase
    {
        private object _selectedItem;
        private ObservableCollection<PropertyBase> _properties = new ObservableCollection<PropertyBase>();

        public PropertiesViewModel()
        {
            PropertyChanged += PropertiesViewModel_PropertyChanged;
            ContentId = "Properties";
        }

        private void PropertiesViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                PopulatePropertySheet();
            }
        }

        private void PopulatePropertySheet()
        {
            Properties.Clear();

        }

        public object SelectedItem {
            get => _selectedItem;
            internal set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PropertyBase> Properties { get => _properties; set { _properties = value; OnPropertyChanged(); } }

    }
}
