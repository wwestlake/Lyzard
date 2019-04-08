using Lyzard.IDE.Messages;
using Lyzard.MessageBus;
using System.Windows.Input;
using System.Windows.Media;


namespace Lyzard.IDE.ViewModels
{
    public class PaneViewModel : ViewModelBase
    {
        public PaneViewModel()
        { }


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


    }
}
