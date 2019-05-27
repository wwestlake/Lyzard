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
using Lyzard.IDE.Messages;
using Lyzard.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels
{
    public abstract class DocumentViewModelBase : PaneViewModel
    {
        private string _filePath;
        protected bool initialLoad = false;
        private bool _isDirty;

        public DocumentViewModelBase()
        {
            _filePath = "Here is a tooltip";
            IsSelected = true;
        }

        public ICommand CloseCommand => new DelegateCommand((x) => {
            Close();
        });

        public bool IsDirty
        {
            get { return _isDirty; }
            set {
                if (!initialLoad)
                    _isDirty = value;
                else
                    initialLoad = false;
            }
        }

        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public abstract bool CanSave(object param);
        public abstract void Save(object param);
        public abstract void SaveAs(object param);
        public abstract void Close();

    }
}
