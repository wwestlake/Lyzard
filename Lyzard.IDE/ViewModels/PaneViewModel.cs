using Lyzard.IDE.Messages;
using Lyzard.MessageBus;
using System;
using System.Windows.Input;
using System.Windows.Media;


namespace Lyzard.IDE.ViewModels
{
    public delegate void ToolWindowHiddenEventHandler(object sender, EventArgs e);

    public class PaneViewModel : ViewModelBase
    {
        public event ToolWindowHiddenEventHandler ToolWindowHidden;


        public PaneViewModel()
        { }

        protected void OnToolWindowHidden()
        {
            ToolWindowHidden?.Invoke(this, new EventArgs());
        }

        #region Title

        private string _title = null;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    this.FirePropertyChanged();
                }
            }
        }

        #endregion

        public ImageSource IconSource
        {
            get;
            protected set;
        }

        #region ContentId

        private string _contentId = null;
        public string ContentId
        {
            get { return _contentId; }
            set
            {
                if (_contentId != value)
                {
                    _contentId = value;
                    FirePropertyChanged();
                }
            }
        }

        #endregion

        #region IsSelected

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    FirePropertyChanged();
                }
            }
        }

        #endregion

        #region IsActive

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    FirePropertyChanged();
                    if (_isActive) MessageBroker.Instance.Publish(this, new DocumentActivatedMessage());
                }
            }
        }

        #endregion

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    FirePropertyChanged();
                }
            }
        }



 
    }
}
