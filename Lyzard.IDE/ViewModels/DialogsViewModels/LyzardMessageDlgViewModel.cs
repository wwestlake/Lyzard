using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lyzard.IDE.ViewModels.DialogsViewModels
{
    public enum LyzardMessageResult { Ok, Yes, No, Cancel, Close }

    public class LyzardMessageDlgViewModel : DialogViewModelBase
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
            set { _title = value; FirePropertyChanged(); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; FirePropertyChanged(); }
        }

        private bool _okVisaible;
        public bool OkVisible
        {
            get { return _okVisaible; }
            set { _okVisaible = value; FirePropertyChanged(); }
        }

        private bool _yesVisible;
        public bool YesVisible
        {
            get { return _yesVisible; }
            set { _yesVisible = value; FirePropertyChanged(); }
        }
        private bool _noVisible;
        public bool NoVisible
        {
            get { return _noVisible; }
            set { _noVisible = value; FirePropertyChanged(); }
        }

        private bool _cancelVisible;
        public bool CancelVisible
        {
            get { return _cancelVisible; FirePropertyChanged(); }
            set { _cancelVisible = value; }
        }

        private bool _textVisible;
        public bool TextVisible
        {
            get { return _textVisible; FirePropertyChanged(); }
            set { _textVisible = value; }
        }

        private bool _listVisible;
        public bool ListVisible
        {
            get { return _listVisible; FirePropertyChanged(); }
            set { _listVisible = value; }
        }

        private ObservableCollection<string> _items = new ObservableCollection<string>();

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set { _items = value; }
        }




    }
}
