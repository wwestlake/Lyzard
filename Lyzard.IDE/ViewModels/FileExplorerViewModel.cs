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
using Lyzard.FileSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Lyzard.IDE.ViewModels
{
    public class FileExplorerViewModel : ExplorerViewModelBase
    {
        private bool _canHide = true;

        public FileExplorerViewModel()
        {
            ContentId = "FileExplorer";
            Title = "File Explorer";
            IconSource = new BitmapImage(new Uri($"pack://application:,,/Resources/Images/FileExplore.png"));
            Items = new ObservableCollection<DirectoryItemViewModel>(FileSystemAccess.GetDrives().ToList().Select( drive => {
                return new DirectoryItemViewModel(drive);
            })); 

        }

        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
            


        public bool CanHide
        {
            get
            {
                return _canHide;
            }
            set
            {
                _canHide = value;
                OnPropertyChanged();
            }
        }

    }
}
