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
using Lyzard.IDE.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels.DialogsViewModels
{
    public class NewProjectDlgViewModel : DialogViewModelBase
    {
        private string _title;

        public NewProjectDlgViewModel()
        {
            Title = "Create Project";
            ProjectPath = CommonFolders.UserProjects;
        }
        
        public ICommand Close => new DelegateCommand((x) => {
            Completed?.Invoke(this);
        });

        public ICommand Create => new DelegateCommand((x) => {
            Completed?.Invoke(this);
        });

        public ICommand SelectFolder => new DelegateCommand((x) => {
            var result = DialogManager.SelectFolder();
            if (!string.IsNullOrEmpty(result))
                ProjectPath = result;
        });


        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _projectPath;

        public string ProjectPath
        {
            get { return _projectPath; }
            set { _projectPath = value; OnPropertyChanged(); }
        }


    }
}
