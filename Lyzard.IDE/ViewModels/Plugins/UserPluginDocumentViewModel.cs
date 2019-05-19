using Lyzard.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lyzard.IDE.ViewModels.Plugins
{
    public class UserPluginDocumentViewModel : DocumentViewModelBase
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
