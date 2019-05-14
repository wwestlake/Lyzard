using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyzard.IDE.ViewModels.SimulationItemViewModels
{
    public class SimFunctionViewModel : ViewModelBase
    {
        private string _title;
        private ObservableCollection<DiagramIOViewModel> _inputs = new ObservableCollection<DiagramIOViewModel>();
        private ObservableCollection<DiagramIOViewModel> _outputs = new ObservableCollection<DiagramIOViewModel>();

        public SimFunctionViewModel()
        {
            Title = "Function";
            _inputs.Add(new DiagramIOViewModel { Name = "Input Value", Value = "0.0" });
            _outputs.Add(new DiagramIOViewModel { Name = "Output Value" });
        }
        public string Title { get { return _title; } set { _title = value; FirePropertyChanged(); } }

        public ObservableCollection<DiagramIOViewModel> InputItems { get => _inputs; set { _inputs = value; FirePropertyChanged(); } }
        public ObservableCollection<DiagramIOViewModel> OutputItems { get => _outputs; set { _outputs = value; FirePropertyChanged(); } }

    }
}
