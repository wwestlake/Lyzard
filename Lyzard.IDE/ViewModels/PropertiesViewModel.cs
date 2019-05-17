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
                FirePropertyChanged();
            }
        }

        public ObservableCollection<PropertyBase> Properties { get => _properties; set { _properties = value; FirePropertyChanged(); } }

    }
}
