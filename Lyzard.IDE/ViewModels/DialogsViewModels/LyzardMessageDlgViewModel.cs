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
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels.DialogsViewModels
{
    internal enum LyzardMessageResult { Ok, Yes, No, Cancel, Close }

    internal class LyzardMessageDlgViewModel : DialogViewModelBase
    {


        public LyzardMessageDlgViewModel()
        {

        }

        public LyzardMessageResult Result { get; set; }

        public ICommand CloseCommand => new DelegateCommand((x) => {
            Result = LyzardMessageResult.Close;
            Completed?.Invoke(this);
        });

        public ICommand OkComand => new DelegateCommand((x) => {
            Result = LyzardMessageResult.Ok;
            Completed?.Invoke(this);
        });
        public ICommand YesCommand => new DelegateCommand((x) => {
            Result = LyzardMessageResult.Yes;
            Completed?.Invoke(this);
        });
        public ICommand NoCommand => new DelegateCommand((x) => {
            Result = LyzardMessageResult.No;
            Completed?.Invoke(this);
        });
        public ICommand CancelCommand => new DelegateCommand((x) => {
            Result = LyzardMessageResult.Cancel;
            Completed?.Invoke(this);
        });

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        private bool _okVisaible;
        public bool OkVisible
        {
            get { return _okVisaible; }
            set { _okVisaible = value; OnPropertyChanged(); }
        }

        private bool _yesVisible;
        public bool YesVisible
        {
            get { return _yesVisible; }
            set { _yesVisible = value; OnPropertyChanged(); }
        }
        private bool _noVisible;
        public bool NoVisible
        {
            get { return _noVisible; }
            set { _noVisible = value; OnPropertyChanged(); }
        }

        private bool _cancelVisible;
        public bool CancelVisible
        {
            get { return _cancelVisible;  }
            set { _cancelVisible = value; OnPropertyChanged(); }
        }

        private bool _textVisible;
        public bool TextVisible
        {
            get { return _textVisible;  }
            set { _textVisible = value; OnPropertyChanged(); }
        }

        private bool _listVisible;
        public bool ListVisible
        {
            get { return _listVisible;  }
            set { _listVisible = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _items = new ObservableCollection<string>();

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set { _items = value; }
        }




    }
}
